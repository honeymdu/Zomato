using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zomato.Data;
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

            // Register Fare Calculation Strategies
            services.AddTransient<IDeliveryFareCalculationStrategy,DeliveryFareDefaultFareCalculationStrategy>();  //You need a new instance every time
            services.AddTransient<IDeliveryFareCalculationStrategy,DeliveryFareSurgePricingFareCalclucationStrategy>();

            // Register Partner Matching Strategies
            services.AddTransient<IDeliveryPartnerMatchingStrategy, DeliveryPartnerMatchingHighestRatingDeliveryPartnerStartegy>();
            services.AddTransient<IDeliveryPartnerMatchingStrategy,DeliveryPartnerMatchingNearestDeliveryPartnerStartegy>();

       
            // Register Strategy Manager
            services.AddSingleton<DeliveryStrategyManager>();
            services.AddSingleton<PaymentStrategyManager>();   // You need one instance for the whole app

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
            services.AddTransient<IOrderRequestService,OrderRequestService>();
            services.AddTransient<IRestaurantService,RestaurantService>();
            services.AddTransient<IWalletTransactionService,WalletTransactionService>();
            services.AddTransient<IDeliveryPartnerService, DeliveryPartnerService>();


            // Register Payment Strategies
            services.AddTransient<IPaymentStrategy, CashPaymentStrategy>();
            services.AddTransient<IPaymentStrategy, WalletPaymentStrategy>();

        }
    }
}
