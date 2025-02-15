using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class WalletDto
    {
        private long id { get; set; }
        private Double Balance { get; set; }
        private UserDto user { get; set; }
        private List<WalletTransactionDto> WalletTransaction { get; set; }
    }
}
