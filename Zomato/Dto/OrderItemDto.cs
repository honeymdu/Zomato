using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class OrderItemDto
    {
        private long id { get; set; }
        private MenuItemDto menuItem { get; set; }
        private int quantity { get; set; }
        private Double totalPrice { get; set; }
    }
}
