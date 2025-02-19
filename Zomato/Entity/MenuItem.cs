using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System;
using Zomato.Entity.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zomato.Model
{
    public class MenuItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public String imageUrl { get; set; }
        public String name { get; set; }
        public String dishDescription { get; set; }
        public Double price { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public FoodCategory foodCategory { get; set; }
        public List<String> ingredients { get; set; }
        public Double rating { get; set; }
        public Boolean isAvailable { get; set; }
        public Menu menu { get; set; }
    }
}
