using Zomato.Dto;

namespace Zomato.Strategies
{
    public interface IDeliveryFareCalculationStrategy
    {
         Task<Double> calculateDeliveryFees(DeliveryFareGetDto deliveryFareGetDto);
    }
}
