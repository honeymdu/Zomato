using System.Runtime.InteropServices;
using System;
using Zomato.Entity.Enum;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace Zomato.Model
{
    public class Order
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public Consumer consumer { get; set; }
        public List<OrderItem> orderItems { get; set; }
        public Double foodAmount { get; set; }
        public Double platformFee { get; set; }
        public Double totalPrice { get; set; }
        public Point pickupLocation { get; set; }
        public Point dropoffLocation { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public OrderStatus orderStatus { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public PaymentMethod paymentMethod { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public PaymentStatus paymentStatus { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime OrderCreationTime { get; set; } = DateTime.Now;
        public Restaurant restaurant { get; set; }
    }
}
