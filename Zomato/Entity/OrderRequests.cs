using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Zomato.Entity.Enum;
using System.ComponentModel.DataAnnotations;

namespace Zomato.Entity
{
    public class OrderRequests
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public Cart cart { get; set; }
        public Double foodAmount { get; set; }
        public Double platformFee { get; set; }
        public Double totalPrice { get; set; }
        public Double deliveryFee { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public OrderRequestStatus orderRequestStatus { get; set; }
        public Restaurant restaurant { get; set; }
        public Consumer consumer { get; set; }
        public Point DropLocation { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public PaymentMethod paymentMethod { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public PaymentStatus paymentStatus { get; set; }
    }
}
