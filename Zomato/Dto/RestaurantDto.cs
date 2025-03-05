using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class RestaurantDto
    {
        public long id { get; set; }
        public String name { get; set; }
        public PointDto restaurantLocation { get; set; }
        public String gstNumber { get; set; }
        public Boolean isAvailable { get; set; }
        public Boolean isVarified { get; set; }
        public RestaurantPartnerDto restaurantPartner { get; set; }
    }
}
