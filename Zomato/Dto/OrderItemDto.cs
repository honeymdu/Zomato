using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class OrderItemDto
    {
        public long id { get; set; }
        public MenuItemDto menuItem { get; set; }
        public int quantity { get; set; }
        public Double totalPrice { get; set; }
    }
}
