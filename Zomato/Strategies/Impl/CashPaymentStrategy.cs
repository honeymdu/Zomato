using System.Threading.Tasks;
using Zomato.Data;
using Zomato.Entity;
using Zomato.Entity.Enum;
using Zomato.Service;
using Zomato.Service.Impl;

namespace Zomato.Strategies.Impl
{
    public class CashPaymentStrategy : IPaymentStrategy
    {
        private readonly WalletService walletService;
        private readonly AppDbContext _context;
        private readonly IDeliveryService deliveryService;
        private const double PLATFORM_COMMISSION = 10.5;

        public CashPaymentStrategy(WalletService walletService, AppDbContext context, IDeliveryService deliveryService)
        {
            this.walletService = walletService;
            _context = context;
            this.deliveryService = deliveryService;
        }

        public async Task ProcessPayment(Payment payment)
        {
            var deliveryRequest = await deliveryService.getDeliveryRequestByOrderId(payment.order.id);
            var deliveryPartner = deliveryRequest.deliveryPartner;
            // Wallet driverwallet = walletService.findWalletById(driver.getId());
            Double platform_commission = payment.amount * PLATFORM_COMMISSION;
            await walletService.deductMoneyFromWallet(deliveryPartner.user, platform_commission, null, payment.order,
                    TransactionMethod.ORDER);

            payment.paymentStatus =PaymentStatus.CONFIRMED;
            _context.Payment.Add(payment);
           await _context.SaveChangesAsync();        
        }
    }
}
