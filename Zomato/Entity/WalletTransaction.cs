using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Zomato.Entity.Enum;

namespace Zomato.Entity
{
    public class WalletTransaction
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public Double Amount { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public TransactionType transactionType { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public TransactionMethod transactionMethod { get; set; }
        public Order order { get; set; }
        public Wallet wallet { get; set; }
        public String TransactionId { get; set; }
        public DateTime timeStamp { get; set; }
    }
}
