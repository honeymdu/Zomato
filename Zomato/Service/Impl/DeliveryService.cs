using System.Collections.Concurrent;
using Zomato.Dto;
using Zomato.Entity.Enum;
using Zomato.Exceptions.CustomExceptionHandler;
using Zomato.Entity;
using Zomato.Service;
using Zomato.Strategies;
using Zomato.Data;
using Microsoft.EntityFrameworkCore;
using Zomato.Service.Impl;

public class DeliveryService : IDeliveryService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<DeliveryService> _logger;
    private readonly ConcurrentQueue<OrderPriorityQueue> _highPriorityOrderQueue = new();
    private readonly ConcurrentQueue<OrderPriorityQueue> _waitingQueue = new();
    private static readonly SemaphoreSlim _semaphore = new(5); // Max concurrent tasks
    private readonly AppDbContext _context;
    private readonly DeliveryStrategyManager deliveryStrategyManager;

    public DeliveryService(IServiceScopeFactory serviceScopeFactory, ILogger<DeliveryService> logger, AppDbContext dbContext)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
        this._context = dbContext;
    }

    public async Task AssignDeliveryPartnerAsync()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var restaurantService = scope.ServiceProvider.GetRequiredService<IRestaurantService>();
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

        _logger.LogInformation("Fetching verified and active restaurants...");
        var restaurants = await restaurantService.GetAllVerifiedAndActiveRestaurants();
        var restaurantPriorityQueue = new ConcurrentQueue<RestaurantPriorityQueue>();

        foreach (var restaurant in restaurants)
        {
            int readyOrders = restaurant.Orders.Count(o => o.orderStatus == OrderStatus.READY_FOR_PICKUP);
            if (readyOrders > 0)
            {
                restaurantPriorityQueue.Enqueue(new RestaurantPriorityQueue(restaurant, readyOrders));
            }
        }

        while (restaurantPriorityQueue.TryDequeue(out var restaurantPriority))
        {
            var orders = restaurantPriority.Restaurant.Orders
                .Where(o => o.orderStatus == OrderStatus.READY_FOR_PICKUP)
                .Select(o => new OrderPriorityQueue(o.id, o.paymentMethod, restaurantPriority.Priority, DateTime.UtcNow.Ticks));

            foreach (var order in orders)
            {
                _highPriorityOrderQueue.Enqueue(order);
            }
        }

        while (_highPriorityOrderQueue.TryDequeue(out var orderPriority))
        {
            _ = Task.Run(async () =>
            {
                await ProcessOrderAsync(orderPriority, orderService);
            });
        }
    }

    private async Task ProcessOrderAsync(OrderPriorityQueue orderPriority, IOrderService orderService)
    {
        await _semaphore.WaitAsync();
        try
        {
            _logger.LogInformation($"Processing order {orderPriority.Id}");
            await SendNotificationToDeliveryPartnerAsync(orderPriority, orderService);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error processing order {orderPriority.Id}: {ex.Message}");
            _waitingQueue.Enqueue(orderPriority);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    private async Task SendNotificationToDeliveryPartnerAsync(OrderPriorityQueue orderPriority, IOrderService orderService)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        // var deliveryRequestRepo = scope.ServiceProvider.GetRequiredService<IDeliveryRequestRepository>();
        var restaurantService = scope.ServiceProvider.GetRequiredService<IRestaurantService>();
        // var deliveryStrategyManager = scope.ServiceProvider.GetRequiredService<IDeliveryStrategyManager>();

        var order = await orderService.GetOrderByIdAsync(orderPriority.Id);
        var restaurant = await restaurantService.getRestaurantByIdAsync(order.restaurant.id);

        var strategy = deliveryStrategyManager.GetDeliveryPartnerMatchingStrategy(restaurant.rating);
        DeliveryFareGetDto deliveryFareGetDto = new DeliveryFareGetDto()
        {
            DropLocation = order.dropoffLocation,
            PickupLocation = order.pickupLocation
        };
        var deliveryPartners = strategy.findMatchingDeliveryPartner(deliveryFareGetDto);

        if (!deliveryPartners.Any())
        {
            _waitingQueue.Enqueue(orderPriority);
            _logger.LogWarning("No delivery partners available.");
            return;
        }

        var deliveryRequest = new DeliveryRequest
        {
            deliveryRequestStatus = DeliveryRequestStatus.PENDING,
            PickupLocation = order.pickupLocation,
            DropLocation = order.dropoffLocation,
            order = order,
            consumerOtp = GenerateRandomOtp(),
            restaurantOtp = GenerateRandomOtp()
        };

        await _context.DeliveryRequest.AddAsync(deliveryRequest);
        _logger.LogInformation($"Created delivery request for order {order.id}");
    }

    private static string GenerateRandomOtp() => new Random().Next(1000, 9999).ToString();

    public async Task<DeliveryRequest> GetDeliveryRequestByOrderIdAsync(long orderId)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        //var deliveryRequestRepo = scope.ServiceProvider.GetRequiredService<IDeliveryRequestRepository>();

        return await _context.DeliveryRequest
                                        .Where(d => d.order.id == orderId)
                                        .FirstOrDefaultAsync()
                ?? throw new ResourceNotFoundException($"Delivery request not found for Order ID {orderId}");
    }

    public async Task<DeliveryRequest> CreateDeliveryRequestAsync(Order order)
    {
        
        // Update order status
        order.orderStatus = OrderStatus.DELIVERY_REQUEST_CREATED;
        await _context.Order.AddAsync(order); // Ensure async call is awaited
        await _context.SaveChangesAsync();
        // Create delivery request
        var deliveryRequest = new DeliveryRequest
        {
            deliveryRequestStatus = DeliveryRequestStatus.PENDING,
            PickupLocation = order.pickupLocation,
            DropLocation = order.dropoffLocation,
            order = order, // Ensure property names match correctly
            consumerOtp = GenerateRandomOtp(),
            restaurantOtp = GenerateRandomOtp()
        };

        // Add delivery request to context and save changes
        await _context.DeliveryRequest.AddAsync(deliveryRequest);
        await _context.SaveChangesAsync();

        return deliveryRequest; // Return the created delivery request
    }


    public DeliveryRequest getDeliveryRequestByOrderId(long id)
    {
      return _context.DeliveryRequest.Find(id)?? throw new ResourceNotFoundException("Request not found");
    }

}
