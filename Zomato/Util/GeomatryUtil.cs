using NetTopologySuite.Geometries;
using Zomato.Dto;

namespace Zomato.Util
{
    public class GeometryUtil
    {
        public static Point CreatePoint(PointDto pointDto)
        {
            GeometryFactory geometryFactory = new GeometryFactory(new PrecisionModel(), 4326);
            Coordinate coordinate = new Coordinate(pointDto.Coordinates[0], pointDto.Coordinates[1]);
            return geometryFactory.CreatePoint(coordinate);
        }
    }
}

