using System.Runtime.InteropServices;
using Zomato.Dto;
using Zomato.Entity.Enum;
using Zomato.Entity;

namespace Zomato.Service
{
    public interface IWalletService
    {

        Task<WalletDto> addMoneyToWallet(User user, Double drivercut, String transactionId, Order order,
                        TransactionMethod transactionMethod);

        Task<Wallet> deductMoneyFromWallet(User user, Double amount, String transactionId, Order order,
                        TransactionMethod transactionMethod);

        Task withdrawAllMyMoneyFromWallet();

        Task<Wallet> findWalletById(long WalletId);

        Task<WalletDto> createNewWallet(User user);

        Task<Wallet> findWalletByUser(User user);

    }
}
