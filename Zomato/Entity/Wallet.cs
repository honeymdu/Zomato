using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Zomato.Model
{
    public class Wallet
    {
        public long id { get; set; }
        public Double Balance { get; set; }
        public User user { get; set; }
        public List<WalletTransaction> WalletTransaction { get; set; }
    }
}
