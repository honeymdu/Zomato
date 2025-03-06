using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class CreateOrderRequestDto
    {
        private PointDto userLocation;

        private PaymentMethod paymentMethod;
    }
}
