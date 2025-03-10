using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Entity;
using Zomato.Entity.Enum;
using Zomato.Exceptions.CustomExceptionHandler;
using Zomato.Strategies;

namespace Zomato.Service.Impl
{
    public class OrderRequestService : IOrderRequestService
    {
        private readonly AppDbContext _context;
        private readonly ICartService cartService;
        private readonly DeliveryStrategyManager deliveryStrategyManager;
        private const double PLATFORM_COMMISSION = 10.5;

        public OrderRequestService(AppDbContext context, ICartService cartService,DeliveryStrategyManager deliveryStrategyManager)
        {
            _context = context;
            this.cartService = cartService;
            this.deliveryStrategyManager = deliveryStrategyManager;
        }

        public async Task<List<OrderRequests>> getAllOrderRequestByRestaurantId(long restaurantId)
        {
            return await _context.OrderRequest.Where(o => o.restaurant.id == restaurantId && o.orderRequestStatus == OrderRequestStatus.PENDING).ToListAsync();
        }

        public async Task<OrderRequests> getOrderRequestById(long OrderRequestId)
        {
            var orderRequest = await _context.OrderRequest.SingleOrDefaultAsync(o => o.id == OrderRequestId);

            return orderRequest ?? throw new ResourceNotFoundException($"Cart not found with ID = {OrderRequestId}");
        }

        public async Task<OrderRequests> OrderRequest(long CartId, PaymentMethod paymentMethod, Point UserLocation)
        {
            Cart cart = await cartService.getCartById(CartId);
            await cartService.isValidCart(cart);
            IDeliveryFareCalculationStrategy deliveryFareCalculationStrategy = deliveryStrategyManager.GetDeliveryFareCalculationStrategy();
            DeliveryFareGetDto deliveryFareGetDto = new DeliveryFareGetDto()
            {
                DropLocation = UserLocation,
                PickupLocation = cart.restaurant.restaurantLocation
            };
            Double delivery_price =await deliveryFareCalculationStrategy.calculateDeliveryFees(deliveryFareGetDto);
            OrderRequests orderRequests = new OrderRequests() { cart = cart,
                consumer = cart.consumer,
                deliveryFee = delivery_price,
                platformFee = PLATFORM_COMMISSION,
                foodAmount = cart.totalPrice,
                orderRequestStatus = OrderRequestStatus.PENDING,
                restaurant = cart.restaurant,
                paymentMethod = paymentMethod,
                paymentStatus = PaymentStatus.PENDING,
                DropLocation = UserLocation,
                totalPrice = cart.totalPrice + delivery_price+ PLATFORM_COMMISSION
            };
            // Send Notification to Corresponding restaurant

            OrderRequests savedOrderRequests = await save(orderRequests);
           await cartService.inValidCart(cart);
            return savedOrderRequests;
        }

        public async Task<OrderRequests> prePaidOrderRequest(long CartId, PaymentMethod paymentMethod, Point UserLocation)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderRequests> save(OrderRequests orderRequests)
        {
           var updateOrderRequest = _context.OrderRequest.Add(orderRequests).Entity;
           await _context.SaveChangesAsync();
            return updateOrderRequest;
        }
    }
}
