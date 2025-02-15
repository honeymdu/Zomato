using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Zomato.Model
{
    public class RestaurantPartner
    {
        public long id { get; set; }
        public long aadharNo { get; set; }
        public User user { get; set; }
    }
}
