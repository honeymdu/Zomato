using NetTopologySuite.Geometries;

namespace Zomato.Service
{
    public interface IDistanceService
    {
        double CalculateDistance(Point src, Point dest);
    }
}
