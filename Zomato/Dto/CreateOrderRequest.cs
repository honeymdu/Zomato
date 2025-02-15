using NetTopologySuite.Geometries;
using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class CreateOrderRequest
    {
        private Point userLocation { get; set; }

        private PaymentMethod paymentMethod { get; set; }
    }
}
