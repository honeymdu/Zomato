﻿using Zomato.Data;
using Zomato.Entity;
using Zomato.Entity.Enum;
using Zomato.Service.Impl;

namespace Zomato.Strategies.Impl
{
    public class WalletPaymentStrategy : IPaymentStrategy
    {
        private readonly WalletService walletService;
        private readonly AppDbContext _context;
        private const double PLATFORM_COMMISSION = 10.5;

        public WalletPaymentStrategy(WalletService walletService, AppDbContext context)
        {
            this.walletService = walletService;
            _context = context;
        }

        public void ProcessPayment(Payment payment)
        {
            RestaurantPartner restaurantPartner = payment.order.restaurant.restaurantPartner;
            Consumer consumer = payment.order.consumer;
            // Wallet driverwallet = walletService.findWalletById(driver.getId());
            Double platform_commission = payment.amount * PLATFORM_COMMISSION;
            walletService.deductMoneyFromWallet(consumer.user, payment.amount, null, payment.order,
                            TransactionMethod.ORDER);

            Double drivercut = payment.amount - platform_commission;

            walletService.addMoneyToWallet(restaurantPartner.user, drivercut, null,
                            payment.order,
                            TransactionMethod.ORDER);
            payment.paymentStatus = PaymentStatus.CONFIRMED;
            _context.Payment.Add(payment);
            _context.SaveChanges();
        }
    }
}
