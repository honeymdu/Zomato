using Azure;
using System.Runtime.InteropServices;
using Zomato.Dto;

namespace Zomato.Service
{
    public interface IAdminService
    {
        RestaurantPartnerDto onBoardNewRestaurantPartner(long UserId,
            OnBoardRestaurantPartnerDto onBoardRestaurantPartnerDto);

        DeliveryPartnerDto onBoardDeliveryPartner(long UserId, OnBoardDeliveryPartnerDto onBoardDeliveryPartnerDto);

        Page<RestaurantDto> getAllRestaurant(PageRequest pageRequest);

        Page<DeliveryPartnerDto> getAllDeliveryPartner(PageRequest pageRequest);

        Boolean varifyRestaurant(long restaurantId);
    }
}
