using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class RestaurantDto
    {
        private long id { get; set; }
        private String name { get; set; }
        private PointDto restaurantLocation { get; set; }
        private String gstNumber { get; set; }
        private Boolean isAvailable { get; set; }
        private Boolean isVarified { get; set; }
        private RestaurantPartnerDto restaurantPartner { get; set; }
    }
}
