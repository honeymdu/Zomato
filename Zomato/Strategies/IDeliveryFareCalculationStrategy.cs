using Zomato.Dto;

namespace Zomato.Strategies
{
    public interface IDeliveryFareCalculationStrategy
    {
         Double calculateDeliveryFees(DeliveryFareGetDto deliveryFareGetDto);
    }
}
