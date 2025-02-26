using System.Runtime.InteropServices;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Service
{
    public interface IMenuService
    {
        Task<Menu> addMenuItem(long RestaurantId, MenuItemDto menuItem);
        Task<Boolean> setMenuItemStatus(long RestaurantId, long menuItemId, Boolean isAvailable);
        Task<Boolean> removeMenuItem(long RestaurantId, long MenuItemId);
        Task<MenuItem> getMenuItemById(long RestaurantId, long MenuItemId);
        Task<Menu> getMenuByRestaurant(long RestaurantId);
        Task<Menu> CreateMenu(CreateMenu createMenu);
        Task<Menu> getMenuById(long MenuItemId);
        Task<Boolean> checkMenuItemExistByName(long RestaurantId, MenuItem MenuItem);

    }
}
