using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class CartDto
    {
        public long id { get; set; }
        public List<CartItemDto> cartItems { get; set; }
        public Double totalPrice { get; set; }
    }
}
