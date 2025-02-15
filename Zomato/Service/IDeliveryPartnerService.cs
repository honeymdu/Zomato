using Azure;
using NetTopologySuite.Geometries;
using System.Runtime.InteropServices;
using Zomato.Dto;
using Zomato.Model;

namespace Zomato.Service
{
    public interface IDeliveryPartnerService
    {

        void rateDeliveryPartner(long UserId, Double rating);

        void acceptDeliveryRequest(long deliveryRequestId);

        void cancelDeliveryRequest(long deliveryRequestId);

        void completeOrderDelivery(long deliveryRequestId, String consumerOtp);

        DeliveryPartnerDto save(DeliveryPartner deliveryPartner);

        Page<DeliveryPartner> getAllDeliveryPartner(PageRequest pageRequest);

        void pickupOrderFromRestaurant(long deliveryRequestId, String restaurantOTP);

        DeliveryPartner getCurrentDeliveryPartner();

        Point getCurrentLocation();

    }
}
