using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zomato.Entity
{
    public class Cart
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        public Consumer consumer { get; set; }

        public Restaurant restaurant { get; set; }

        public List<CartItem> cartItems { get; set; }

        public Double totalPrice { get; set; }

        public Boolean validCart { get; set; }
    }
}
