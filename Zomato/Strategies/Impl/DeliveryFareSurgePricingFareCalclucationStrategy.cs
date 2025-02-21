using Zomato.Dto;
using Zomato.Service;

namespace Zomato.Strategies.Impl
{
    public class DeliveryFareSurgePricingFareCalclucationStrategy : IDeliveryFareCalculationStrategy
    {
        private static readonly Double SURGE_FACTOR = 2.0;
        private readonly IDistanceService distanceService;
        public static readonly Double RIDE_FARE_MULTIPLYIER = 10.0;

        public DeliveryFareSurgePricingFareCalclucationStrategy(IDistanceService distanceService)
        {
            this.distanceService = distanceService;
        }

        public double calculateDeliveryFees(DeliveryFareGetDto deliveryFareGetDto)
        {
            double distance = distanceService.CalculateDistance(deliveryFareGetDto.PickupLocation,
              deliveryFareGetDto.DropLocation);
            return RIDE_FARE_MULTIPLYIER * SURGE_FACTOR * distance;
        }
    }
}
