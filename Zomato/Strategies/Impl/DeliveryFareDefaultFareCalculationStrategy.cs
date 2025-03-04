using System.Threading.Tasks;
using Zomato.Dto;
using Zomato.Service;

namespace Zomato.Strategies.Impl
{
    public class DeliveryFareDefaultFareCalculationStrategy : IDeliveryFareCalculationStrategy
    {
        private readonly IDistanceService distanceService;
        public static readonly Double RIDE_FARE_MULTIPLYIER = 10.0;

        public DeliveryFareDefaultFareCalculationStrategy(IDistanceService distanceService)
        {
            this.distanceService = distanceService;
        }

        public async Task<double> calculateDeliveryFees(DeliveryFareGetDto deliveryFareGetDto)
        {
            double distance = await distanceService.CalculateDistance(deliveryFareGetDto.PickupLocation,
               deliveryFareGetDto.DropLocation);
            return RIDE_FARE_MULTIPLYIER * distance;
        }
    }
}
