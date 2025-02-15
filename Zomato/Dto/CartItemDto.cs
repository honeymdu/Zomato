using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class CartItemDto
    {
        private long id { get; set; }
        private int quantity { get; set; }
        private MenuItemDto menuItem { get; set; }
        private Double totalPrice { get; set; }
    }
}
