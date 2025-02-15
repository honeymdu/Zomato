using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class PreOrderRequestDto
    {
        private long consumerId { get; set; }
        private long restaurantId { get; set; }
        private CartDto cart { get; set; }
        private Double foodAmount { get; set; }
        private Double platformFee { get; set; }
        private Double deliveryFee { get; set; }
        private Double totalPrice { get; set; }
        private PointDto currentLocation { get; set; }
    }
}
