using NetTopologySuite.Geometries;

namespace Zomato.Service
{
    public interface IDistanceService
    {
        Task<double> CalculateDistance(Point src, Point dest);
    }
}
