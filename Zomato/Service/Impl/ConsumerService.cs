using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Entity;
using Zomato.Entity.Enum;
using Zomato.Exceptions.CustomExceptionHandler;

namespace Zomato.Service.Impl
{
    public class ConsumerService : IConsumerService
    {
        private readonly IOrderRequestService orderRequestService;
        private readonly IRestaurantService restaurantService;
        private readonly IMapper mapper;
        private readonly IMenuService menuService;
        private readonly ICartService cartService;
        private readonly ICartItemService cartItemService;
       // private readonly PreOrderRequestService preOrderRequestService;
        private readonly IDeliveryService deliveryService;
        private readonly AppDbContext context;
        private readonly UserContextService userContextService;

        public ConsumerService(IOrderRequestService orderRequestService, IRestaurantService restaurantService, IMapper mapper, IMenuService menuService, ICartService cartService, ICartItemService cartItemService, IDeliveryService deliveryService, AppDbContext context, UserContextService userContextService)
        {
            this.orderRequestService = orderRequestService;
            this.restaurantService = restaurantService;
            this.mapper = mapper;
            this.menuService = menuService;
            this.cartService = cartService;
            this.cartItemService = cartItemService;
            this.deliveryService = deliveryService;
            this.context = context;
            this.userContextService = userContextService;
        }

        public async Task clearCart(long RestaurantId)
        {
            Consumer consumer = await getCurrentConsumer();
            Cart cart = await cartService.getCartByConsumerIdAndRestaurantId(consumer.id, RestaurantId);
            await cartService.deleteAllCartItemByCartId(cart.id);
        }

        public async Task<Consumer> createNewConsumer(User user)
        {
            Consumer consumer = new Consumer()
            {
                user = user,
                rating = 0.0
            };
           var savedConsumer = context.Consumer.Add(consumer).Entity;
            await context.SaveChangesAsync();
            return savedConsumer;
        }

        public async Task<OrderRequestsDto> createOrderRequest(long RestaurantId, CreateOrderRequest createOrderRequest)
        {
            PaymentMethod paymentMethod = createOrderRequest.paymentMethod;
            Point UserLocation = createOrderRequest.userLocation;
            Consumer consumer = await getCurrentConsumer();
            Cart cart = await cartService.getCartByConsumerIdAndRestaurantId(consumer.id, RestaurantId);
            OrderRequests orderRequests =await orderRequestService.OrderRequest(cart.id, paymentMethod, UserLocation);
            return mapper.Map<OrderRequestsDto>(orderRequests);
        }

        public async Task<Page<Restaurant>> getAllRestaurant(PageRequest pageRequest)
        {
            return (Page<Restaurant>)await restaurantService.getAllVarifiedRestaurant(pageRequest.PageNumber,pageRequest.PageSize);
        }

        public async Task<Consumer> getConsumerById(long consumerId)
        {
            return await context.Consumer.FindAsync(consumerId) ?? throw new ResourceNotFoundException("Consumer Not Found"+consumerId);
        }

        public async Task<Consumer> getCurrentConsumer()
        {
            var userEmail = userContextService.GetUserEmail();
            var user = await context.User.Where(u => u.email == userEmail).FirstOrDefaultAsync();
            return await context.Consumer.Where(rp => rp.user == user).FirstOrDefaultAsync() ?? throw new ResourceNotFoundException("Current User not Found");
        }

        public async Task<ConsumerOTP> getOtpByOrderId(long OrderId)
        {
            DeliveryRequest deliveryRequest = await deliveryService.getDeliveryRequestByOrderId(OrderId);
            return new ConsumerOTP() { consumerOTP = deliveryRequest.consumerOtp };
        }

        public async Task<CartDto> PrepareCart(long RestaurantId, long MenuItemId)
        {
            Consumer consumer = await getCurrentConsumer();
            return await cartService.prepareCart(consumer, RestaurantId, MenuItemId);
        }

        public bool PreProcessPayment()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> rateRestaurant(long RestaurantId, double rating)
        {
            Restaurant restaurant = await restaurantService.getRestaurantById(RestaurantId);
            restaurant.rating = rating;
            await restaurantService.save(restaurant);
            return true;
        }

        public async Task<CartDto> removeCartItem(long CartId, long cartItemId)
        {
            Consumer consumer = await getCurrentConsumer();
            Cart cart = await cartService.getCartById(CartId);
            await cartService.isValidCartExist(consumer, cart.restaurant.id);
            CartItem cartItem = await cartItemService.getCartItemById(cartItemId);
            return await cartService.removeItemFromCart(CartId, cartItem);
        }

        public async Task<CartDto> viewCart(long RestaurantId)
        {
            Consumer consumer = await getCurrentConsumer();
            Cart cart = await cartService.getCartByConsumerIdAndRestaurantId(consumer.id, RestaurantId);
            return await cartService.viewCart(cart.id);
        }

        public async Task<Menu> viewMenuByRestaurantId(long RestaurantId)
        {
            return await menuService.getMenuByRestaurant(RestaurantId);
        }

        public PreOrderRequestDto viewPreOrderRequest(long RestaurantId, Point UserLocation)
        {
            throw new NotImplementedException();
        }
    }
}
