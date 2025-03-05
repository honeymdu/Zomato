using NetTopologySuite.Geometries;
using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class CreateOrderRequest
    {
        public Point userLocation { get; set; }

        public PaymentMethod paymentMethod { get; set; }
    }
}
