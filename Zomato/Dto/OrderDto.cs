using System.Runtime.InteropServices;
using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class OrderDto
    {
        private long id { get; set; }
        private ConsumerDto consumer { get; set; }
        private List<OrderItemDto> orderItems { get; set; }
        private Double totalPrice { get; set; }
        private Double foodAmount { get; set; }
        private Double platformFee { get; set; }
        private PointDto pickupLocation { get; set; }
        private PointDto dropoffLocation { get; set; }
        private OrderStatus orderStatus { get; set; }
        private DateTime OrderCreationTime { get; set; }
        private RestaurantDto restaurant { get; set; }
        private DeliveryPartnerDto deliveryPartner { get; set; }
    }
}
