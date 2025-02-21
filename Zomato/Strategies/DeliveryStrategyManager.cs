using Zomato.Strategies.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Zomato.Strategies
{
        public class DeliveryStrategyManager
        {
            private readonly IServiceProvider _serviceProvider;

            public DeliveryStrategyManager(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            public IDeliveryFareCalculationStrategy GetDeliveryFareCalculationStrategy()
            {
                TimeOnly surgeStartTime = new(18, 0, 0);  // 6 PM
                TimeOnly surgeEndTime = new(21, 0, 0);    // 9 PM
                TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);

                bool isSurgeTime = currentTime >= surgeStartTime && currentTime <= surgeEndTime;

                return isSurgeTime
                    ? _serviceProvider.GetRequiredService<DeliveryFareSurgePricingFareCalclucationStrategy>()
                    : _serviceProvider.GetRequiredService<DeliveryFareDefaultFareCalculationStrategy>();
            }

            public IDeliveryPartnerMatchingStrategy GetDeliveryPartnerMatchingStrategy(double restaurantRating)
            {
                return restaurantRating >= 4.8
                    ? _serviceProvider.GetRequiredService<DeliveryPartnerMatchingHighestRatingDeliveryPartnerStartegy>()
                    : _serviceProvider.GetRequiredService<DeliveryPartnerMatchingNearestDeliveryPartnerStartegy>();
            }
        }
}


