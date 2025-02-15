namespace Zomato.Dto
{
    public class PointDto
    {

        public double[] Coordinates { get; }  // Public getter

        public string Type { get; } = "Point";  // Read-only property

        public PointDto(double[] coordinates)
        {
            Coordinates = coordinates;
        }

    }
}
