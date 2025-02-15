using Zomato.Model;

namespace Zomato.Dto
{
    public class CreateMenu
    {
        private String name { get; set; }
        private List<MenuItemDto> menuItem { get; set; }
        private Restaurant restaurant { get; set; }

    }
}
