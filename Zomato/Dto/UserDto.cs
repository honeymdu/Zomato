using System.Collections.Generic;
using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class UserDto
    {

        public String name { get; set; }
        public String email { get; set; }
        public Role role { get; set; }
        public String contact { get; set; }
        public List<AddressDto> addresses { get; set; }

    }
}
