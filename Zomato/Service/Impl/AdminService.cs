using Azure;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Entity.Enum;
using Zomato.Exceptions.CustomExceptionHandler;
using Zomato.Entity;
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

        public Task<Page<DeliveryPartnerDto>> getAllDeliveryPartner(PageRequest pageRequest)
        {
            throw new NotImplementedException();
        }

        public Task<Page<RestaurantDto>> getAllRestaurant(PageRequest pageRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<DeliveryPartnerDto> onBoardDeliveryPartner(long UserId, OnBoardDeliveryPartnerDto onBoardDeliveryPartnerDto)
        {
            User user = await _userService.getUserFromId(UserId);
            if (user.role.Equals(Role.DELIVERY_PARTNER))
            {
                throw new RuntimeConfilictException("User is Already a DeliveryPartner with userID +" + UserId);
            }

            user.role = Role.DELIVERY_PARTNER;

            DeliveryPartner deliveryPartner = _mapper.Map<DeliveryPartner>(onBoardDeliveryPartnerDto);
            deliveryPartner.user = user;
            deliveryPartner.available = true;
            deliveryPartner.rating = 0.0;
            return await deliveryPartnerService.save(deliveryPartner);
        }

        public async Task<RestaurantPartnerDto> onBoardNewRestaurantPartner(long UserId, OnBoardRestaurantPartnerDto onBoardRestaurantPartnerDto)
        {
            var user = await _userService.getUserFromId(UserId);

            if (user == null)
            {
                throw new ResourceNotFoundException($"User with ID {UserId} not found.");
            }

            if (user.role == Role.RESTAURENT_PARTNER)
            {
                throw new RuntimeConfilictException($"User is already a RESTAURANT_PARTNER with userID {UserId}");
            }

            user.role = Role.RESTAURENT_PARTNER;

            var restaurantPartner = _mapper.Map<RestaurantPartner>(onBoardRestaurantPartnerDto);
            restaurantPartner.user = user;

            var savedRestaurantPartner = await restaurantPartnerService.createNewRestaurantPartner(restaurantPartner);

            return _mapper.Map<RestaurantPartnerDto>(savedRestaurantPartner);
        }

        public async Task<bool> varifyRestaurant(long restaurantId)
        {
            Restaurant restaurant = await restaurantService.getRestaurantById(restaurantId);
            // check if already varified.
            if (restaurant.isVarified)
            {
                throw new RuntimeConfilictException("Restaurant is Already Varified with Restaurant Id = " + restaurantId);
            }
            restaurant.isVarified = true;
            await restaurantService.save(restaurant);
            return true;
        }
    }
}
