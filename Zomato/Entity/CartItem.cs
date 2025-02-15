using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System;
using System.Numerics;

namespace Zomato.Model
{
    public class CartItem
    {
        public long id { get; set; }
        public MenuItem menuItem { get; set; }
        public int quantity { get; set; }
        public Double totalPrice { get; set; }
        public Cart cart { get; set; }
    }
}
