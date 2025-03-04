using Azure;
using NetTopologySuite.Geometries;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Service.Impl
{
    public class DeliveryPartnerService : IDeliveryPartnerService
    {
        public void acceptDeliveryRequest(long deliveryRequestId)
        {
            throw new NotImplementedException();
        }

        public void cancelDeliveryRequest(long deliveryRequestId)
        {
            throw new NotImplementedException();
        }

        public void completeOrderDelivery(long deliveryRequestId, string consumerOtp)
        {
            throw new NotImplementedException();
        }

        public Page<DeliveryPartner> getAllDeliveryPartner(PageRequest pageRequest)
        {
            throw new NotImplementedException();
        }

        public DeliveryPartner getCurrentDeliveryPartner()
        {
            throw new NotImplementedException();
        }

        public Point getCurrentLocation()
        {
            throw new NotImplementedException();
        }

        public void pickupOrderFromRestaurant(long deliveryRequestId, string restaurantOTP)
        {
            throw new NotImplementedException();
        }

        public void rateDeliveryPartner(long UserId, double rating)
        {
            throw new NotImplementedException();
        }

        public Task<DeliveryPartnerDto> save(DeliveryPartner deliveryPartner)
        {
            throw new NotImplementedException();
        }
    }
}
