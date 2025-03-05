using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class CartItemDto
    {
        public long id { get; set; }
        public int quantity { get; set; }
        public MenuItemDto menuItem { get; set; }
        public Double totalPrice { get; set; }
    }
}
