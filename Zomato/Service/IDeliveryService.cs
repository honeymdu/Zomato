
using Zomato.Entity;

namespace Zomato.Service
{
    public interface IDeliveryService
    {
       
        Task AssignDeliveryPartnerAsync();

        Task<DeliveryRequest> CreateDeliveryRequestAsync(Order order);
        DeliveryRequest getDeliveryRequestByOrderId(long id);
        Task<DeliveryRequest> GetDeliveryRequestByOrderIdAsync(long orderId);
    }
}
