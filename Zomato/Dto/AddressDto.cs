namespace Zomato.Dto
{
    public class AddressDto
    {
        public String street { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public String postalCode { get; set; }
        public PointDto userLocation { get; set; }
    }
}
