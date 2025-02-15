using System.Runtime.InteropServices;
using Zomato.Dto;
using Zomato.Entity.Enum;
using Zomato.Model;

namespace Zomato.Service
{
    public interface IWalletService
    {

        WalletDto addMoneyToWallet(User user, Double drivercut, String transactionId, Order order,
                        TransactionMethod transactionMethod);

        Wallet deductMoneyFromWallet(User user, Double platform_commission, String transactionId, Order order,
                        TransactionMethod transactionMethod);

        void withdrawAllMyMoneyFromWallet();

        Wallet findWalletById(long WalletId);

        WalletDto createNewWallet(User user);

        Wallet findWalletByUser(User user);

    }
}
