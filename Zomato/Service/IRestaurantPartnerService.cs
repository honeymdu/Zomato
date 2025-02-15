using System.Runtime.InteropServices;
using Zomato.Dto;
using Zomato.Entity.Enum;
using Zomato.Model;

namespace Zomato.Service
{
    public interface IRestaurantPartnerService
    {

        RestaurantDto createRestaurant(AddNewRestaurantDto addNewRestaurantDto);

        Menu CreateMenu(CreateMenu createMenu);

        Order acceptOrderRequest(long orderRequestId);

        OrderRequestsDto cancelOrderRequest(long orderRequestId);

        Menu updateMenuItemOfMenu(MenuItemDto menuItemDto);

        RestaurantPartner getCurrentRestaurantPartner();

        Menu addMenuItemToMenu(MenuItemDto menuItemDto, long restaurantId);

        List<WalletTransaction> getAllMyWalletTransactions(long restaurantId);

        RestaurantPartner createNewRestaurantPartner(RestaurantPartner restaurantPartner);

        Restaurant ViewMyRestaurantProfile(long RestaurantId);

        List<OrderRequests> viewOrderRequestsByRestaurantId(long RestaurantId);

        Menu viewMenuByRestaurantId(long RestaurantId);

        Order updateOrderStatus(long OrderId, OrderStatus orderStatus);

        // RestaurantDto updateRestaurantStatus(RestaurantStatusDto restaurantStatusDto,
        // Long restaurantId);

        // MenuDto updateMenuStatus(MenuStatusDto menuStatusDto, Long menuId);

        // MenuItemDto updateMenuItemStatus(MenuItemStatusDto menuItemStatusDto, Long
        // menuItemId);

        // Page<WalletTransactionDto> getWalletTransactions(PageRequest pageRequest);

        // List<Order> getALlOrders(Long restaurantId);

        RestaurantOTP getRestaurantOTPByOrderId(long OrderId);
    }
}
