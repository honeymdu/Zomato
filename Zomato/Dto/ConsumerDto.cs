using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class ConsumerDto
    {
        private long id { get; set; }
        private UserDto user { get; set; }
        private Double rating { get; set; }
    }
}
