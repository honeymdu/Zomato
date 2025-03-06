using Azure;
using NetTopologySuite.Geometries;
using System.Runtime.InteropServices;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Service
{
    public interface IDeliveryPartnerService
    {

        Task rateDeliveryPartner(long UserId, Double rating);

        Task acceptDeliveryRequest(long deliveryRequestId);

        Task cancelDeliveryRequest(long deliveryRequestId);

        Task completeOrderDelivery(long deliveryRequestId, String consumerOtp);

        Task<DeliveryPartnerDto> save(DeliveryPartner deliveryPartner);

        Page<DeliveryPartner> getAllDeliveryPartner(PageRequest pageRequest);

        Task pickupOrderFromRestaurant(long deliveryRequestId, String restaurantOTP);

        Task<DeliveryPartner> getCurrentDeliveryPartner();

        Task<Point> getCurrentLocation();

    }
}
