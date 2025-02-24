using Zomato.Entity;

namespace Zomato.Dto
{
    public class CreateMenu
    {
        public String name { get; set; }
        public List<MenuItemDto> menuItem { get; set; }
        public Restaurant restaurant { get; set; }

    }
}
