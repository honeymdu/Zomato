using System.Runtime.InteropServices;
using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class WalletTransactionDto
    {
        private long id { get; set; }
        private Double Amount { get; set; }
        private TransactionType transactionType { get; set; }
        private TransactionMethod transactionMethod { get; set; }
        private OrderDto order { get; set; }
        private WalletDto wallet { get; set; }
        private DateTime timeStamp { get; set; }
    }
}
