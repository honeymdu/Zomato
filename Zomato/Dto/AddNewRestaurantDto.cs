namespace Zomato.Dto
{
    public class AddNewRestaurantDto
    {
        private string name { get; set; }
        private PointDto restaurantLocation { get; set; }
        private string gstNumber { get; set; }
    }
}
