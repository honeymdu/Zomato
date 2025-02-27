using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Entity;
using Zomato.Entity.Enum;

namespace Zomato.Service.Impl
{
    public class RestaurantPartnerService : IRestaurantPartnerService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IOrderRequestService orderRequestService;
        private readonly IOrderService orderService;
        private readonly IPaymentService paymentService;
        private readonly IMenuService menuService;
        private readonly IRestaurantService restaurantService;
        private readonly IWalletTransactionService walletTransactionService;
        private readonly IDeliveryService deliveryService;

        public RestaurantPartnerService(IMapper mapper, AppDbContext context, IOrderRequestService orderRequestService, IOrderService orderService, IPaymentService paymentService, IMenuService menuService, IRestaurantService restaurantService)
        {
            _mapper = mapper;
            _context = context;
            this.orderRequestService = orderRequestService;
            this.orderService = orderService;
            this.paymentService = paymentService;
            this.menuService = menuService;
            this.restaurantService = restaurantService;
        }

        public async Task<Order> acceptOrderRequest(long orderRequestId)
        {
            OrderRequests orderRequests = await orderRequestService.getOrderRequestById(orderRequestId);
            // check order status
            if (orderRequests.orderRequestStatus.Equals(OrderRequestStatus.PENDING))
            {
                orderRequests.orderRequestStatus = OrderRequestStatus.ACCEPTED;
                OrderRequests saveOrderRequests = await orderRequestService.save(orderRequests);
                Order order = await orderService.createOrder(saveOrderRequests);
                await paymentService.CreateNewPayment(order);
                return order;

            }

            throw new Exception(
                    "Can not accept the order Request as the order Request status ="
                            + orderRequests.orderRequestStatus);
        }

        public async Task<Menu> addMenuItemToMenu(MenuItemDto menuItemDto, long restaurantId)
        {
            return await menuService.addMenuItem(restaurantId, menuItemDto);
        }

        public Task<OrderRequestsDto> cancelOrderRequest(long orderRequestId)
        {
            throw new NotImplementedException();
        }

        public async Task<Menu> CreateMenu(CreateMenu createMenu)
        {
            return await menuService.CreateMenu(createMenu);
        }

        public async Task<RestaurantPartner> createNewRestaurantPartner(RestaurantPartner restaurantPartner)
        {
            _context.RestaurantPartner.Add(restaurantPartner);
           await _context.SaveChangesAsync();
            return restaurantPartner;

        }

        public async Task<RestaurantDto> createRestaurant(AddNewRestaurantDto addNewRestaurantDto)
        {
            RestaurantDto restaurant = _mapper.Map<RestaurantDto>(addNewRestaurantDto);
            Restaurant savedRestaurant = await restaurantService.AddNewRestaurant( await getCurrentRestaurantPartner(), restaurant);
            return _mapper.Map<RestaurantDto>(savedRestaurant);
        }

        public async Task<List<WalletTransaction>> getAllMyWalletTransactions(long restaurantId)
        {
            RestaurantPartner restaurantPartner = await getCurrentRestaurantPartner();
            return await walletTransactionService.getAllWalletTransactionByUser(restaurantPartner.user);
        }

        public async Task<RestaurantPartner> getCurrentRestaurantPartner()
        {
            return null;

        }

        public async Task<RestaurantOTP> getRestaurantOTPByOrderId(long OrderId)
        {
            var OTP = await deliveryService.getDeliveryRequestByOrderId(OrderId);

            return new RestaurantOTP()
            {

                restaurantOTP = OTP.restaurantOtp

            };
        }

        public async Task<Menu> updateMenuItemOfMenu(MenuItemDto menuItemDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> updateOrderStatus(long OrderId, OrderStatus orderStatus)
        {
            Order order = await orderService.getOrderById(OrderId);
            order.orderStatus =orderStatus;
            return await orderService.saveOrder(order);
        }

        public async Task<Menu> viewMenuByRestaurantId(long RestaurantId)
        {
            return await menuService.getMenuByRestaurant(RestaurantId);
        }

        public async Task<Restaurant> ViewMyRestaurantProfile(long RestaurantId)
        {
            RestaurantPartner restaurantPartner = await getCurrentRestaurantPartner();
            Restaurant restaurant = await restaurantService.viewProfile(RestaurantId);
            if (!restaurant.restaurantPartner.Equals(restaurantPartner))
            {
                throw new Exception("Restaurant is not assositated with the current resturant partner");
            }
            return restaurant;
        }

        public async Task<List<OrderRequests>> viewOrderRequestsByRestaurantId(long RestaurantId)
        {
            return await orderRequestService.getAllOrderRequestByRestaurantId(RestaurantId);
        }
    }
}
