using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using NetTopologySuite.Geometries;

namespace Zomato.Entity
{
    public class DeliveryPartner
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public Double rating { get; set; }
        public User user { get; set; }
        public Boolean available { get; set; }
        public String vehicleId { get; set; }
        public Point currentLocation { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public List<DeliveryRequest> deliveryRequest { get; set; }
    }
}
