using Azure;
using NetTopologySuite.Geometries;
using System.Runtime.InteropServices;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Service
{
    public interface IConsumerService
    {
        OrderRequestsDto createOrderRequest(long RestaurantId, CreateOrderRequest createOrderRequest);

       // PrePaidOrderRequestsDto createPrePaidOrderRequest(Long RestaurantId, CreateOrderRequest createOrderRequest);

        Boolean rateRestaurant(long RestaurantId, Double rating);
        Consumer createNewConsumer(User user);
        Consumer getConsumerById(long consumerId);
        Page<Restaurant> getAllRestaurant(PageRequest pageRequest);
        Consumer getCurrentConsumer();
        CartDto viewCart(long RestaurantId);
        CartDto PrepareCart(long RestaurantId, long MenuItemId);
        CartDto removeCartItem(long CartId, long cartItemId);
        void clearCart(long RestaurantId);
        Menu viewMenuByRestaurantId(long RestaurantId);
        PreOrderRequestDto viewPreOrderRequest(long RestaurantId, Point UserLocation);
        Boolean PreProcessPayment();
        ConsumerOTP getOtpByOrderId(long OrderId);
    }
}
