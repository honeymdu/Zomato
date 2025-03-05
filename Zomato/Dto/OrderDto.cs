using System.Runtime.InteropServices;
using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class OrderDto
    {
        public long id { get; set; }
        public ConsumerDto consumer { get; set; }
        public List<OrderItemDto> orderItems { get; set; }
        public Double totalPrice { get; set; }
        public Double foodAmount { get; set; }
        public Double platformFee { get; set; }
        public PointDto pickupLocation { get; set; }
        public PointDto dropoffLocation { get; set; }
        public OrderStatus orderStatus { get; set; }
        public DateTime OrderCreationTime { get; set; }
        public RestaurantDto restaurant { get; set; }
        public DeliveryPartnerDto deliveryPartner { get; set; }
    }
}
