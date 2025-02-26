using Azure;
using System.Runtime.InteropServices;
using Zomato.Dto;

namespace Zomato.Service
{
    public interface IAdminService
    {
        Task<RestaurantPartnerDto> onBoardNewRestaurantPartner(long UserId,
            OnBoardRestaurantPartnerDto onBoardRestaurantPartnerDto);

        Task<DeliveryPartnerDto> onBoardDeliveryPartner(long UserId, OnBoardDeliveryPartnerDto onBoardDeliveryPartnerDto);

        Task<Page<RestaurantDto>> getAllRestaurant(PageRequest pageRequest);

        Task<Page<DeliveryPartnerDto>> getAllDeliveryPartner(PageRequest pageRequest);

        Task<Boolean> varifyRestaurant(long restaurantId);
    }
}
