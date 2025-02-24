using System.Runtime.InteropServices;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zomato.Entity
{
    public class OrderItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public MenuItem menuItem { get; set; }
        public Int64 quantity { get; set; }
        public Double totalPrice { get; set; }
        public Order order { get; set; }
    }
}
