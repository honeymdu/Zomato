using NetTopologySuite.Geometries;
using Zomato.Dto;
using System;

namespace Zomato.Util
{
    public class GeometryUtil
    {
        public static Point CreatePoint(PointDto pointDto)
        {
            if (pointDto == null || pointDto.Coordinates == null || pointDto.Coordinates.Length < 2)
            {
                throw new ArgumentException("Invalid coordinates in PointDto.");
            }

            GeometryFactory geometryFactory = new GeometryFactory(new PrecisionModel(), 4326);
            Coordinate coordinate = new Coordinate(pointDto.Coordinates[0], pointDto.Coordinates[1]);
            return geometryFactory.CreatePoint(coordinate);
        }
    }
}
