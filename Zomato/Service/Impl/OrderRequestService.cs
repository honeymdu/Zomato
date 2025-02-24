using Microsoft.AspNetCore.Cors.Infrastructure;
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

        public List<OrderRequests> getAllOrderRequestByRestaurantId(long restaurantId)
        {
            return _context.OrderRequest.Where(o => o.restaurant.id == restaurantId && o.orderRequestStatus == OrderRequestStatus.PENDING).ToList();
        }

        public OrderRequests getOrderRequestById(long OrderRequestId)
        {
            var orderRequest = _context.OrderRequest.SingleOrDefault(o => o.id == OrderRequestId);

            return orderRequest ?? throw new ResourceNotFoundException($"Cart not found with ID = {OrderRequestId}");
        }

        public OrderRequests OrderRequest(long CartId, PaymentMethod paymentMethod, Point UserLocation)
        {
            Cart cart = cartService.getCartById(CartId);
            cartService.isValidCart(cart);
            IDeliveryFareCalculationStrategy deliveryFareCalculationStrategy = deliveryStrategyManager.GetDeliveryFareCalculationStrategy();
            DeliveryFareGetDto deliveryFareGetDto = new DeliveryFareGetDto()
            {
                DropLocation = UserLocation,
                PickupLocation = cart.restaurant.restaurantLocation
            };
            Double delivery_price = deliveryFareCalculationStrategy.calculateDeliveryFees(deliveryFareGetDto);
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

            OrderRequests savedOrderRequests = save(orderRequests);
            cartService.inValidCart(cart);
            return savedOrderRequests;
        }

        public OrderRequests prePaidOrderRequest(long CartId, PaymentMethod paymentMethod, Point UserLocation)
        {
            throw new NotImplementedException();
        }

        public OrderRequests save(OrderRequests orderRequests)
        {
            _context.OrderRequest.Add(orderRequests);
            _context.SaveChanges();
            return orderRequests;
        }
    }
}
