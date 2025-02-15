namespace Zomato.Dto
{
    public class AddressDto
    {
        private String street { get; set; }
        private String city { get; set; }
        private String state { get; set; }
        private String postalCode { get; set; }
        private PointDto userLocation { get; set; }
    }
}
