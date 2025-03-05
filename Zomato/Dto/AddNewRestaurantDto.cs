namespace Zomato.Dto
{
    public class AddNewRestaurantDto
    {
        public string name { get; set; }
        public PointDto restaurantLocation { get; set; }
        public string gstNumber { get; set; }
    }
}
