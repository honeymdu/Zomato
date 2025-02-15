using System.Runtime.InteropServices;
using System;

namespace Zomato.Model
{
    public class OrderItem
    {
        public long id { get; set; }
        public MenuItem menuItem { get; set; }
        public Int64 quantity { get; set; }
        public Double totalPrice { get; set; }
        public Order order { get; set; }
    }
}
