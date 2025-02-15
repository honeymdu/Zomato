using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class DeliveryPartnerDto
    {
        private long id { get; set; }
        private Double rating { get; set; }
        private UserDto user { get; set; }
        private Boolean available { get; set; }
        private String vehicleId { get; set; }
        private PointDto currentLocation { get; set; }
        private List<DeliveryItemDto> deliveryItems { get; set; }
    }
}
