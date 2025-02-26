using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Strategies
{
    public interface IDeliveryPartnerMatchingStrategy
    {
        Task<List<DeliveryPartner>> findMatchingDeliveryPartner(DeliveryFareGetDto deliveryFareGetDto);
    }
}
