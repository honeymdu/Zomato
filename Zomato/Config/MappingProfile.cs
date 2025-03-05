using AutoMapper;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Config
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();  // Ensure this mapping exists
            CreateMap<Cart, CartDto>();
            CreateMap<DeliveryPartner, DeliveryPartnerDto>();
            CreateMap<Restaurant, RestaurantDto>();
            CreateMap<Wallet, WalletDto>();
        }
    }
}
