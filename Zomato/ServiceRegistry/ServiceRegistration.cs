using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zomato.Data;
using Zomato.Exceptions;
using Zomato.Service;
using Zomato.Service.Impl;
using Zomato.Strategies;
using Zomato.Strategies.Impl;
using Zomato.Strategies.Zomato.Strategies;

namespace Zomato.ServiceRegistry
{
    public static class ServiceRegistration
    {
        public static void AddStrategyServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();

            // Register Application Services
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ICartItemService, CartItemService>();
            services.AddTransient<ICartService, CartService>();
            services.AddHttpClient<IDistanceService, DistanceService>();
            services.AddTransient<IDeliveryService, DeliveryService>();
            services.AddTransient<IWalletService, WalletService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderRequestService, OrderRequestService>();
            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IWalletTransactionService, WalletTransactionService>();
            services.AddTransient<IDeliveryPartnerService, DeliveryPartnerService>();
            services.AddTransient<IRestaurantPartnerService, RestaurantPartnerService>();

            // Register Payment Services
            services.AddTransient<IPaymentService, PaymentService>();

            // Register Payment Strategies
            services.AddTransient<CashPaymentStrategy>();
            services.AddTransient<WalletPaymentStrategy>();
            services.AddTransient<IEnumerable<IPaymentStrategy>>(sp => new List<IPaymentStrategy>
            {
                sp.GetRequiredService<CashPaymentStrategy>(),
                sp.GetRequiredService<WalletPaymentStrategy>()
            });

            // Register Fare Calculation Strategies
            services.AddTransient<DeliveryFareDefaultFareCalculationStrategy>();
            services.AddTransient<DeliveryFareSurgePricingFareCalclucationStrategy>();

            services.AddTransient<IDeliveryFareCalculationStrategy, DeliveryFareDefaultFareCalculationStrategy>();
            services.AddTransient<IDeliveryFareCalculationStrategy, DeliveryFareSurgePricingFareCalclucationStrategy>();

            // Register Partner Matching Strategies
            services.AddTransient<DeliveryPartnerMatchingHighestRatingDeliveryPartnerStartegy>();
            services.AddTransient<DeliveryPartnerMatchingNearestDeliveryPartnerStartegy>();

            services.AddTransient<IDeliveryPartnerMatchingStrategy, DeliveryPartnerMatchingHighestRatingDeliveryPartnerStartegy>();
            services.AddTransient<IDeliveryPartnerMatchingStrategy, DeliveryPartnerMatchingNearestDeliveryPartnerStartegy>();

            // Register Strategy Manager
            services.AddSingleton<DeliveryStrategyManager>();
            services.AddSingleton<PaymentStrategyManager>();

            
        }
    }
}
