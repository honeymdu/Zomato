using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Zomato.Entity.Enum;

namespace Zomato.Entity
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public String name { get; set; }
        public String email { get; set; }
        public String password { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public Role role { get; set; }
        public String contact { get; set; }
        public List<Address> addresses { get; set; }
    }
}
