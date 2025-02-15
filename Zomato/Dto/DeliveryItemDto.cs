using System.Runtime.InteropServices;
using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class DeliveryItemDto
    {
        private long id { get; set; }
        private OrderStatus orderStatus { get; set; }
        private String deliveryAddress { get; set; }
        private DateTime deliveryTime { get; set; }
        private OrderDto order { get; set; }
        private OrderItemDto orderItem { get; set; }
        private DeliveryPartnerDto deliveryPartner { get; set; }
    }
}
