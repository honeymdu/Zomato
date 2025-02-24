using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Zomato.Entity
{
    public class Restaurant
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public String name { get; set; }
        public Point restaurantLocation { get; set; }
        public String gstNumber { get; set; }
        public Double rating { get; set; }
        public Boolean isAvailable { get; set; }
        public Boolean isVarified { get; set; }
        public RestaurantPartner restaurantPartner { get; set; }
        public List<Order> Orders { get; set; }
        public List<OrderRequests> orderRequests { get; set; }
    }
}
