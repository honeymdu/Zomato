using System.Runtime.InteropServices;
using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class DeliveryItemDto
    {
        public long id { get; set; }
        public OrderStatus orderStatus { get; set; }
        public String deliveryAddress { get; set; }
        public DateTime deliveryTime { get; set; }
        public OrderDto order { get; set; }
        public OrderItemDto orderItem { get; set; }
        public DeliveryPartnerDto deliveryPartner { get; set; }
    }
}
