using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Strategies
{
    public interface IDeliveryPartnerMatchingStrategy
    {
        List<DeliveryPartner> findMatchingDeliveryPartner(DeliveryFareGetDto deliveryFareGetDto);
    }
}
