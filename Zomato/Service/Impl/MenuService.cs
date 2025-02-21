using AutoMapper;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Exceptions.CustomExceptionHandler;
using Zomato.Model;

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

        public Menu addMenuItem(long RestaurantId, MenuItemDto menuItemDto)
        {
            MenuItem menuItem = _mapper.Map<MenuItem>(menuItemDto);
            if (checkMenuItemExistByName(RestaurantId, menuItem)) {
            throw new RuntimeConfilictException("Menu Item already exist with Menu Item Name " + menuItem.name);
            }
            Menu menu = getMenuByRestaurant(RestaurantId);
            menuItem.menu = menu;
            menuItem.rating = 0.0;
            _context.MenuItem.Add(menuItem);
           return _context.Menu.Update(menu).Entity;
        }

        public bool checkMenuItemExistByName(long RestaurantId, MenuItem MenuItem)
        {
            Menu menu = getMenuByRestaurant(RestaurantId);
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

        public Menu CreateMenu(CreateMenu createMenu)
        {
            List<MenuItem> menuItems = _mapper.Map<List<MenuItem>>(createMenu.menuItem);

        
            Menu menu = new Menu()
            {
                menuItems = menuItems,
                menuName = createMenu.name,
                restaurant = createMenu.restaurant
            };

            _context.Add(menu);
            _context.SaveChanges();
            return menu;
        }

        public Menu getMenuById(long MenuId)
        {
            return _context.Menu.Find(MenuId) ?? throw new ResourceNotFoundException("Menu Not found");
        }

        public Menu getMenuByRestaurant(long RestaurantId)
        {
            return _context.Menu.Where(m =>m.restaurant.id == RestaurantId).FirstOrDefault() ?? throw new ResourceNotFoundException("Menu Not found");
        }

        public MenuItem getMenuItemById(long RestaurantId, long MenuItemId)
        {
            Menu menu = getMenuByRestaurant(RestaurantId);
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

        public bool removeMenuItem(long RestaurantId, long MenuItemId)
        {
            Menu menu = getMenuByRestaurant(RestaurantId);
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

        public bool setMenuItemStatus(long RestaurantId, long menuItemId, bool isAvailable)
        {
            Menu menu = getMenuByRestaurant(RestaurantId);
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
