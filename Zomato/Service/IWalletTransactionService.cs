using Zomato.Entity;

namespace Zomato.Service
{
    public interface IWalletTransactionService
    {

        Task CreateNewWalletTransaction(WalletTransaction WalletTransaction);

        Task<List<WalletTransaction>> getAllWalletTransactionByUser(User user);

    }
}
