using System.Runtime.InteropServices;
using Zomato.Model;

namespace Zomato.Service
{
    public interface IDeliveryService
    {
        void AssignDeliveryPartner();
        DeliveryRequest createDeliveryRequest(Order order);
        DeliveryRequest getDeliveryRequestByOrderId(long orderId);
    }
}
