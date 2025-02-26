using System.Runtime.InteropServices;
using NetTopologySuite.Geometries;
using Zomato.Entity;
using Zomato.Entity.Enum;
namespace Zomato.Service
{
    public interface IOrderRequestService
    {
        Task<OrderRequests> save(OrderRequests orderRequests);
        Task<OrderRequests> getOrderRequestById(long OrderRequestId);
        Task<List<OrderRequests>> getAllOrderRequestByRestaurantId(long restaurantId);
        Task<OrderRequests> OrderRequest(long CartId, PaymentMethod paymentMethod, Point UserLocation);
        Task<OrderRequests> prePaidOrderRequest(long CartId, PaymentMethod paymentMethod, Point UserLocation);
    }
}
