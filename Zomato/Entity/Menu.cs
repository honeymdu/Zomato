using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Zomato.Entity
{
    public class Menu
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public String menuName { get; set; }
        public List<MenuItem> menuItems { get; set; }
        public Restaurant restaurant { get; set; }
    }
}
