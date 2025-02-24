using Zomato.Dto;
using Zomato.Entity.Enum;
using Zomato.Entity;

namespace Zomato.Service.Impl
{
    public class WalletService : IWalletService
    {
        public WalletDto addMoneyToWallet(User user, double drivercut, string transactionId, Order order, TransactionMethod transactionMethod)
        {
            throw new NotImplementedException();
        }

        public WalletDto createNewWallet(User user)
        {
            throw new NotImplementedException();
        }

        public Wallet deductMoneyFromWallet(User user, double platform_commission, string transactionId, Order order, TransactionMethod transactionMethod)
        {
            throw new NotImplementedException();
        }

        public Wallet findWalletById(long WalletId)
        {
            throw new NotImplementedException();
        }

        public Wallet findWalletByUser(User user)
        {
            throw new NotImplementedException();
        }

        public void withdrawAllMyMoneyFromWallet()
        {
            throw new NotImplementedException();
        }
    }
}
