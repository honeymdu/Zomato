using System.Runtime.InteropServices;
using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class OrderRequestsDto
    {
        public long id { get; set; }
        public CartDto cart { get; set; }
        public Double foodAmount { get; set; }
        public Double platformFee { get; set; }
        public Double totalPrice { get; set; }
        public Double deliveryFee { get; set; }
        public OrderRequestStatus orderRequestStatus { get; set; }
        public PaymentMethod paymentMethod { get; set; }
        public PaymentStatus paymentStatus { get; set; }
    }
}
