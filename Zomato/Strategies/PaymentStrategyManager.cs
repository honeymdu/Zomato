using Microsoft.Extensions.DependencyInjection;
using Zomato.Entity.Enum;
using Zomato.Strategies.Impl;

namespace Zomato.Strategies
{
    using Microsoft.Extensions.DependencyInjection;

    namespace Zomato.Strategies
    {
        public class PaymentStrategyManager
        {
            private readonly IServiceProvider _serviceProvider;

            public PaymentStrategyManager(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            public IPaymentStrategy GetPaymentStrategy(PaymentMethod paymentMethod)
            {
                return paymentMethod switch
                {
                    PaymentMethod.WALLET => _serviceProvider.GetRequiredService<WalletPaymentStrategy>(),
                    PaymentMethod.CASH => _serviceProvider.GetRequiredService<CashPaymentStrategy>(),
                    _ => throw new InvalidOperationException("Invalid Payment Method")
                };
            }
        }
    }

}
