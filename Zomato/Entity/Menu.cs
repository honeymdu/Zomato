using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Zomato.Model
{
    public class Menu
    {
        public long id { get; set; }
        public String menuName { get; set; }
        public List<MenuItem> menuItems { get; set; }
        public Restaurant restaurant { get; set; }
    }
}
