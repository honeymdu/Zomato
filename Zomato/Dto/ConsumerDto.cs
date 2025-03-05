using System.Runtime.InteropServices;

namespace Zomato.Dto
{
    public class ConsumerDto
    {
        public long id { get; set; }
        public UserDto user { get; set; }
        public Double rating { get; set; }
    }
}
