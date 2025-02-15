using System.Runtime.InteropServices;
using System;

namespace Zomato.Model
{
    public class Cart
    {
  
        public long id { get; set; }

        public Consumer consumer { get; set; }

        public Restaurant restaurant { get; set; }

        public List<CartItem> cartItems { get; set; }

        public Double totalPrice { get; set; }

        public Boolean validCart { get; set; }
    }
}
