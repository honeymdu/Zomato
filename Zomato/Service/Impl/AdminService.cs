using Azure;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Entity.Enum;
using Zomato.Exceptions.CustomExceptionHandler;
using Zomato.Model;
using AutoMapper;

namespace Zomato.Service.Impl
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRestaurantPartnerService restaurantPartnerService;
        private readonly IDeliveryPartnerService deliveryPartnerService;
        private readonly IRestaurantService restaurantService;

        public AdminService(AppDbContext context, IConfiguration config, IUserService userService, IMapper mapper, IDeliveryPartnerService deliveryPartnerService   )
        {
            _context = context;
            _config = config;
            _userService = userService;
            _mapper = mapper;
            this.deliveryPartnerService = deliveryPartnerService;
        }

        public Page<DeliveryPartnerDto> getAllDeliveryPartner(PageRequest pageRequest)
        {
            throw new NotImplementedException();
        }

        public Page<RestaurantDto> getAllRestaurant(PageRequest pageRequest)
        {
            throw new NotImplementedException();
        }

        public DeliveryPartnerDto onBoardDeliveryPartner(long UserId, OnBoardDeliveryPartnerDto onBoardDeliveryPartnerDto)
        {
            User user = _userService.getUserFromId(UserId);
            if (user.role.Equals(Role.DELIVERY_PARTNER))
            {
                throw new RuntimeConfilictException("User is Already a DeliveryPartner with userID +" + UserId);
            }

            user.role = Role.DELIVERY_PARTNER;

            DeliveryPartner deliveryPartner = _mapper.Map<DeliveryPartner>(onBoardDeliveryPartnerDto);
            deliveryPartner.user = user;
            deliveryPartner.available = true;
            deliveryPartner.rating = 0.0;
            return deliveryPartnerService.save(deliveryPartner);
        }

        public RestaurantPartnerDto onBoardNewRestaurantPartner(long UserId, OnBoardRestaurantPartnerDto onBoardRestaurantPartnerDto)
        {
            User user = _userService.getUserFromId(UserId);
            if (user.role.Equals(Role.RESTAURENT_PARTNER))
            {
                throw new RuntimeConfilictException("User is Already a RESTAURENT_PARTNER with userID +" + UserId);
            }

            user.role = Role.RESTAURENT_PARTNER;

            RestaurantPartner restaurantPartner = _mapper.Map<RestaurantPartner>(onBoardRestaurantPartnerDto);
            restaurantPartner.user = user;
            RestaurantPartner savedrestaurantPartner = restaurantPartnerService
                    .createNewRestaurantPartner(restaurantPartner);
            return _mapper.Map<RestaurantPartnerDto>(savedrestaurantPartner);
        }

        public bool varifyRestaurant(long restaurantId)
        {
            Restaurant restaurant = restaurantService.getRestaurantById(restaurantId);
            // check if already varified.
            if (restaurant.isVarified)
            {
                throw new RuntimeConfilictException("Restaurant is Already Varified with Restaurant Id = " + restaurantId);
            }
            restaurant.isVarified = true;
            restaurantService.save(restaurant);
            return true;
        }
    }
}
