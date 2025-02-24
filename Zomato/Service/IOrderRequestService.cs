using System.Runtime.InteropServices;
using NetTopologySuite.Geometries;
using Zomato.Entity;
using Zomato.Entity.Enum;
namespace Zomato.Service
{
    public interface IOrderRequestService
    {
        OrderRequests save(OrderRequests orderRequests);
        OrderRequests getOrderRequestById(long OrderRequestId);
        List<OrderRequests> getAllOrderRequestByRestaurantId(long restaurantId);
        OrderRequests OrderRequest(long CartId, PaymentMethod paymentMethod, Point UserLocation);
        OrderRequests prePaidOrderRequest(long CartId, PaymentMethod paymentMethod, Point UserLocation);
    }
}
