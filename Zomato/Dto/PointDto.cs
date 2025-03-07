using System.Text.Json.Serialization;

namespace Zomato.Dto
{
    public class PointDto
    {
        public double[] Coordinates { get; }  // Read-only property
        public string Type { get; } = "Point";  // Read-only property with a default value

        [JsonConstructor]  // Mark this constructor to be used during deserialization
        public PointDto(double[] coordinates)
        {
            Coordinates = coordinates;
        }
    }
}
