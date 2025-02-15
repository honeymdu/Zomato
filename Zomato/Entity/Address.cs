using System.Runtime.InteropServices;
using System;
using NetTopologySuite.Geometries;

namespace Zomato.Model
{
    public class Address
    {
     
        public long id { get; set; }
        public String street { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public String postalCode { get; set; }
        private Point userLocation { get; set; }
        private User user { get; set; }
    }
}
