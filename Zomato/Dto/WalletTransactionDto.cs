using System.Runtime.InteropServices;
using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class WalletTransactionDto
    {
        public long id { get; set; }
        public Double Amount { get; set; }
        public TransactionType transactionType { get; set; }
        public TransactionMethod transactionMethod { get; set; }
        public OrderDto order { get; set; }
        public WalletDto wallet { get; set; }
        public DateTime timeStamp { get; set; }
    }
}
