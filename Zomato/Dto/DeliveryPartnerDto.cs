using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class DeliveryPartnerDto
    {
        public long id { get; set; }
        public Double rating { get; set; }
        public UserDto user { get; set; }
        public Boolean available { get; set; }
        public String vehicleId { get; set; }
        public PointDto currentLocation { get; set; }
        public List<DeliveryItemDto> deliveryItems { get; set; }
    }
}
