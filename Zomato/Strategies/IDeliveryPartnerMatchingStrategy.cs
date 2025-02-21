using Zomato.Dto;
using Zomato.Model;

namespace Zomato.Strategies
{
    public interface IDeliveryPartnerMatchingStrategy
    {
        List<DeliveryPartner> findMatchingDeliveryPartner(DeliveryFareGetDto deliveryFareGetDto);
    }
}
