using NetTopologySuite.Geometries;

namespace Zomato.Dto
{
    public class DeliveryFareGetDto
    {
        public Point PickupLocation { get; set; }
        public Point DropLocation { get; set; }
    }
}
