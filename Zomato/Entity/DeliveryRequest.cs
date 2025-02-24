using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using NetTopologySuite.Geometries;
using Zomato.Entity.Enum;

namespace Zomato.Entity
{
    public class DeliveryRequest
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public Point PickupLocation { get; set; }
        public Point DropLocation { get; set; }
        public DateTime deliveryTime { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public DeliveryRequestStatus deliveryRequestStatus { get; set; }
        public Order order { get; set; }
        public DeliveryPartner deliveryPartner { get; set; }
        public String restaurantOtp { get; set; }
        public String consumerOtp { get; set; }
    }
}
