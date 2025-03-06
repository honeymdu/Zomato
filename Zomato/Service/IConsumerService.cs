using Azure;
using NetTopologySuite.Geometries;
using System.Runtime.InteropServices;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Service
{
    public interface IConsumerService
    {
        Task<OrderRequestsDto> createOrderRequest(long RestaurantId, CreateOrderRequest createOrderRequest);

       // PrePaidOrderRequestsDto createPrePaidOrderRequest(Long RestaurantId, CreateOrderRequest createOrderRequest);

        Task<Boolean> rateRestaurant(long RestaurantId, Double rating);
        Task<Consumer> createNewConsumer(User user);
        Task<Consumer> getConsumerById(long consumerId);
        Task<Page<Restaurant>> getAllRestaurant(PageRequest pageRequest);
        Task<Consumer> getCurrentConsumer();
        Task<CartDto> viewCart(long RestaurantId);
        Task<CartDto> PrepareCart(long RestaurantId, long MenuItemId);
        Task<CartDto> removeCartItem(long CartId, long cartItemId);
        Task clearCart(long RestaurantId);
        Task<Menu> viewMenuByRestaurantId(long RestaurantId);
        PreOrderRequestDto viewPreOrderRequest(long RestaurantId, Point UserLocation);
        Boolean PreProcessPayment();
        Task<ConsumerOTP> getOtpByOrderId(long OrderId);
    }
}
