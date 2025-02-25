using System.Runtime.InteropServices;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Service
{
    public interface IMenuService
    {
        Menu addMenuItem(long RestaurantId, MenuItemDto menuItem);
        Boolean setMenuItemStatus(long RestaurantId, long menuItemId, Boolean isAvailable);
        Boolean removeMenuItem(long RestaurantId, long MenuItemId);
        MenuItem getMenuItemById(long RestaurantId, long MenuItemId);
        Task<Menu> getMenuByRestaurant(long RestaurantId);
        Menu CreateMenu(CreateMenu createMenu);
        Menu getMenuById(long MenuItemId);
        Boolean checkMenuItemExistByName(long RestaurantId, MenuItem MenuItem);

    }
}
