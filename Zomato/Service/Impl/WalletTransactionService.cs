using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zomato.Data;
using Zomato.Entity;
using Zomato.Exceptions.CustomExceptionHandler;

namespace Zomato.Service.Impl
{
    public class WalletTransactionService : IWalletTransactionService
    {
        private readonly AppDbContext _context;

        public WalletTransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateNewWalletTransaction(WalletTransaction WalletTransaction)
        {
            _context.Add(WalletTransaction);
           await _context.SaveChangesAsync();
        }

        public async Task<List<WalletTransaction>> getAllWalletTransactionByUser(User user)
        {
            Wallet wallet = await _context.Wallet.Where(u => u.user == user).FirstOrDefaultAsync() ?? throw new ResourceNotFoundException("Wallet not found with User Id  " + user.id);
            return await _context.WalletTransaction.Where(u => u.wallet.id == wallet.id).ToListAsync();
        }
    }
}
