using System.Runtime.InteropServices;
using Zomato.Dto;
using Zomato.Entity;
using Zomato.Entity.Enum;

namespace Zomato.Service
{
    public interface IRestaurantPartnerService
    {

        Task<RestaurantDto> createRestaurant(AddNewRestaurantDto addNewRestaurantDto);

        Task<Menu> CreateMenu(CreateMenu createMenu);

        Task<Order> acceptOrderRequest(long orderRequestId);

        Task<OrderRequestsDto> cancelOrderRequest(long orderRequestId);

        Task<Menu> updateMenuItemOfMenu(MenuItemDto menuItemDto);

        Task<RestaurantPartner> getCurrentRestaurantPartner();

        Task<Menu> addMenuItemToMenu(MenuItemDto menuItemDto, long restaurantId);

        Task<List<WalletTransaction>> getAllMyWalletTransactions(long restaurantId);

        Task<RestaurantPartner> createNewRestaurantPartner(RestaurantPartner restaurantPartner);

        Task<Restaurant> ViewMyRestaurantProfile(long RestaurantId);

        Task<List<OrderRequests>> viewOrderRequestsByRestaurantId(long RestaurantId);

        Task<Menu> viewMenuByRestaurantId(long RestaurantId);

        Task<Order> updateOrderStatus(long OrderId, OrderStatus orderStatus);

        // RestaurantDto updateRestaurantStatus(RestaurantStatusDto restaurantStatusDto,
        // Long restaurantId);

        // MenuDto updateMenuStatus(MenuStatusDto menuStatusDto, Long menuId);

        // MenuItemDto updateMenuItemStatus(MenuItemStatusDto menuItemStatusDto, Long
        // menuItemId);

        // Page<WalletTransactionDto> getWalletTransactions(PageRequest pageRequest);

        // List<Order> getALlOrders(Long restaurantId);

        Task<RestaurantOTP> getRestaurantOTPByOrderId(long OrderId);
    }
}
