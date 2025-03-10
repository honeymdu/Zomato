using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Entity;
using Zomato.Exceptions.CustomExceptionHandler;

namespace Zomato.Service.Impl
{
    public class MenuService : IMenuService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MenuService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Menu> addMenuItem(long RestaurantId, MenuItemDto menuItemDto)
        {
            MenuItem menuItem = _mapper.Map<MenuItem>(menuItemDto);
            if (await checkMenuItemExistByName(RestaurantId, menuItem)) {
            throw new RuntimeConfilictException("Menu Item already exist with Menu Item Name " + menuItem.name);
            }
            Menu menu = await getMenuByRestaurant(RestaurantId);
            menuItem.menu = menu;
            menuItem.rating = 0.0;
            _context.MenuItem.Add(menuItem);
            var updateMenu = _context.Menu.Update(menu).Entity;
            await _context.SaveChangesAsync();
            return updateMenu;
        }

        public async Task<bool> checkMenuItemExistByName(long RestaurantId, MenuItem MenuItem)
        {
            Menu menu = await getMenuByRestaurant(RestaurantId);
            List<MenuItem> menuItems = menu.menuItems;
            foreach (MenuItem menuItem in menuItems)
            {
                if (menuItem.id.Equals(MenuItem.name))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<Menu> CreateMenu(CreateMenu createMenu)
        {
            List<MenuItem> menuItems = _mapper.Map<List<MenuItem>>(createMenu.menuItem);
            Menu menu = new Menu()
            {
                menuItems = menuItems,
                menuName = createMenu.name,
                restaurant = createMenu.restaurant
            };

            var updateMenu = _context.Add(menu).Entity;
            await _context.SaveChangesAsync();
            return updateMenu;
        }

        public async Task<Menu> getMenuById(long MenuId)
        {
            return await _context.Menu.FindAsync(MenuId) ?? throw new ResourceNotFoundException("Menu Not found");
        }

        public async Task<Menu> getMenuByRestaurant(long RestaurantId)
        {
            return await _context.Menu.Where(m =>m.restaurant.id == RestaurantId).FirstOrDefaultAsync() ?? throw new ResourceNotFoundException("Menu Not found");
        }

        public async Task<MenuItem> getMenuItemById(long RestaurantId, long MenuItemId)
        {
            Menu menu = await getMenuByRestaurant(RestaurantId);
            List<MenuItem> menuItems = menu.menuItems;
            foreach (MenuItem menuItem in menuItems)
            {
                if (menuItem.id.Equals(MenuItemId))
                {
                    return menuItem;
                }
            }
            throw new ResourceNotFoundException("Menu Item not exist with menuItem Id =" + MenuItemId);

        }

        public async Task<bool> removeMenuItem(long RestaurantId, long MenuItemId)
        {
            Menu menu = await getMenuByRestaurant(RestaurantId);
            List<MenuItem> menuItems = menu.menuItems;
            foreach (MenuItem menuItem in menuItems)
            {
                if (menuItem.id.Equals(MenuItemId))
                {
                    menuItems.Remove(menuItem);
                    return true;
                }
            }

            throw new ResourceNotFoundException("Menu Item Not Exist by Menu Item Id =" + MenuItemId);
        }

        public async Task<bool> setMenuItemStatus(long RestaurantId, long menuItemId, bool isAvailable)
        {
            Menu menu = await getMenuByRestaurant(RestaurantId);
            List<MenuItem> menuItems = menu.menuItems;
            foreach (MenuItem menuItem in menuItems)
            {
                if (menuItem.id.Equals(menuItemId))
                {
                    menuItem.isAvailable= isAvailable;
                    return true;
                }
            }
            return false;
        }
    }
}
