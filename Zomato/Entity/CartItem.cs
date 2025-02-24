using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System;
using System.Numerics;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zomato.Entity
{
    public class CartItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public MenuItem menuItem { get; set; }
        public int quantity { get; set; }
        public Double totalPrice { get; set; }
        public Cart cart { get; set; }
    }
}
