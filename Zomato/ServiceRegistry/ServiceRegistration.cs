using Microsoft.Extensions.DependencyInjection;
using Zomato.Service;
using Zomato.Service.Impl;
using Zomato.Strategies;
using Zomato.Strategies.Impl;
using Zomato.Strategies.Zomato.Strategies;

namespace Zomato.ServiceRegistry
{
    public static class ServiceRegistration
    {
        public static void AddStrategyServices(this IServiceCollection services)
        {
            // Register Fare Calculation Strategies
            services.AddTransient<IDeliveryFareCalculationStrategy,DeliveryFareDefaultFareCalculationStrategy>();  //You need a new instance every time
            services.AddTransient<IDeliveryFareCalculationStrategy,DeliveryFareSurgePricingFareCalclucationStrategy>();

            // Register Partner Matching Strategies
            services.AddTransient<IDeliveryPartnerMatchingStrategy, DeliveryPartnerMatchingHighestRatingDeliveryPartnerStartegy>();
            services.AddTransient<IDeliveryPartnerMatchingStrategy,DeliveryPartnerMatchingNearestDeliveryPartnerStartegy>();

            // Register Payment Strategies
            services.AddTransient<IPaymentStrategy,CashPaymentStrategy>();
            services.AddTransient<IPaymentStrategy,WalletPaymentStrategy>();

            // Register Strategy Manager
            services.AddSingleton<DeliveryStrategyManager>();
            services.AddSingleton<PaymentStrategyManager>();   // You need one instance for the whole app

            // Register Application Services
            services.AddTransient<IAuthService, AuthService>();  
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
