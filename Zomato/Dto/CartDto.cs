using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class CartDto
    {
        private long id { get; set; }
        private List<CartItemDto> cartItems { get; set; }
        private Double totalPrice { get; set; }
    }
}
