using Zomato.Dto;
using Zomato.Entity.Enum;
using Zomato.Entity;
using AutoMapper;
using Zomato.Data;
using Zomato.Exceptions.CustomExceptionHandler;
using Microsoft.EntityFrameworkCore;

namespace Zomato.Service.Impl
{
    public class WalletService : IWalletService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWalletTransactionService walletTransactionService;

        public WalletService(AppDbContext context, IMapper mapper, IWalletTransactionService walletTransactionService)
        {
            _context = context;
            _mapper = mapper;
            this.walletTransactionService = walletTransactionService;
        }

        public async Task<WalletDto> addMoneyToWallet(User user, double drivercut, string transactionId, Order order, TransactionMethod transactionMethod)
        {
            Wallet wallet = await findWalletByUser(user);
            wallet.Balance = wallet.Balance + drivercut;

            WalletTransaction walletTransaction = new WalletTransaction()
            {
                TransactionId = transactionId,
                order = order,
                wallet = wallet,
                transactionType = TransactionType.CREDIT,
                transactionMethod = transactionMethod,
                Amount = drivercut,
            };

            await walletTransactionService
                    .CreateNewWalletTransaction(walletTransaction);
           var saved_Wallet = _context.Wallet.Update(wallet).Entity;
            await _context.SaveChangesAsync();
            return _mapper.Map<WalletDto>(saved_Wallet);
        }

        public async Task<WalletDto> createNewWallet(User user)
        {
            Wallet wallet = new Wallet();
            wallet.user =user;
            wallet.Balance =0.0;
            var updateWallet =_context.Wallet.Update(wallet).Entity;
            await _context.SaveChangesAsync();
            return _mapper.Map<WalletDto>(updateWallet);
        }

        public async Task<Wallet> deductMoneyFromWallet(User user, double amount, string transactionId, Order order, TransactionMethod transactionMethod)
        {
            Wallet wallet = await findWalletByUser(user);
            wallet.Balance = wallet.Balance - amount;
            WalletTransaction walletTransaction = new WalletTransaction()
            {
                TransactionId = transactionId,
                order = order,
                wallet = wallet,
                transactionType = TransactionType.DEBIT,
                transactionMethod = transactionMethod,
                Amount = amount,

            };

            // wallet.getWalletTransaction().add(walletTransaction);
           await walletTransactionService
                    .CreateNewWalletTransaction(walletTransaction);
           var Update_Wallet = _context.Wallet.Update(wallet).Entity;
           await _context.SaveChangesAsync();
            return Update_Wallet;
        }

        public async Task<Wallet> findWalletById(long WalletId)
        {
          return await _context.Wallet.FindAsync(WalletId) ?? throw new ResourceNotFoundException("Wallet Not Found with id "+ WalletId);
            
        }

        public async Task<Wallet> findWalletByUser(User user)
        {
            return await _context.Wallet.Where(u => u.user == user).FirstOrDefaultAsync() ?? throw new ResourceNotFoundException("Wallet Not Found with User "+user);
        }

        public Task withdrawAllMyMoneyFromWallet()
        {
            throw new NotImplementedException();
        }
    }
}
