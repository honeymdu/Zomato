using System.Runtime.InteropServices;
using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class OrderRequestsDto
    {
        private long id { get; set; }
        private CartDto cart { get; set; }
        private Double foodAmount { get; set; }
        private Double platformFee { get; set; }
        private Double totalPrice { get; set; }
        private Double deliveryFee { get; set; }
        private OrderRequestStatus orderRequestStatus { get; set; }
        private PaymentMethod paymentMethod { get; set; }
        private PaymentStatus paymentStatus { get; set; }
    }
}
