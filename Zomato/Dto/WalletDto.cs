using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class WalletDto
    {
       public long id { get; set; }
       public Double Balance { get; set; }
       public UserDto user { get; set; }
       public List<WalletTransactionDto> WalletTransaction { get; set; }
    }
}
