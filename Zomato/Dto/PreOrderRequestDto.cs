using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class PreOrderRequestDto
    {
        public long consumerId { get; set; }
        public long restaurantId { get; set; }
        public CartDto cart { get; set; }
        public Double foodAmount { get; set; }
        public Double platformFee { get; set; }
        public Double deliveryFee { get; set; }
        public Double totalPrice { get; set; }
        public PointDto currentLocation { get; set; }
    }
}
