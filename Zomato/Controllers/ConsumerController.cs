using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Entity;
using Zomato.Service;
using Zomato.Service.Impl;

namespace Zomato.Controllers
{

    [Route("api/consumer")]
    [ApiController]
    [Authorize(Roles = "CONSUMER")]
    public class ConsumerController:ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IConsumerService consumerService;
        private readonly IUserService userService;
        private readonly AppDbContext _context;

        public ConsumerController(IMapper mapper, IConsumerService consumerService, IUserService userService, AppDbContext appDbContext)
        {
            _mapper = mapper;
            this.consumerService = consumerService;
            this.userService = userService;
            _context = appDbContext;
        }

        [HttpPost("prepareCart/{RestaurantId}/{MenuItemId}")]
        public async Task<ActionResult<CartDto>> prepareMyCart([FromRoute] long RestaurantId, [FromRoute] long MenuItemId)
        {
            CartDto cart = await consumerService.PrepareCart(RestaurantId, MenuItemId);
            return Ok(cart);
        }

        [HttpPost("update-address/{UserId}")]
        public async Task<ActionResult> addNewAddress([FromRoute] long UserId,
                        [FromBody] AddressDto addressDto)
        {
            // check user exist
            User user = await userService.getUserFromId(UserId);
            Address address = _mapper.Map<Address>(addressDto);
            address.user = user;
            _context.Add(address);
            await _context.SaveChangesAsync();
            return Ok(address);

        }

        [HttpPost("remove-cartItem/{cartId}/{cartItemId}")]
        public async Task<ActionResult<CartDto>> removeCartItem([FromRoute] long cartId, [FromRoute] long cartItemId)
        {
            CartDto cart = await consumerService.removeCartItem(cartId, cartItemId);
            return Ok(cart);
        }

        [HttpPost("clearCartItem/{RestaurantId}")]
        public async Task<ActionResult> ClearCartItem([FromRoute] long RestaurantId)
        {
             await consumerService.clearCart(RestaurantId);
            return NoContent();
        }


        [HttpPost("view-pre-order-request/{RestaurantId}")]
        public ActionResult<PreOrderRequestDto> viewPreOrderRequest([FromRoute] long RestaurantId,
                        [FromBody] CreateOrderRequestDto createOrderRequestDto)
        {
            CreateOrderRequest createOrderRequest = _mapper.Map<CreateOrderRequest>(createOrderRequestDto);
                return Ok(consumerService.viewPreOrderRequest(RestaurantId,
                                createOrderRequest.userLocation));
        }


        [HttpPost("create-order-request/{RestaurantId}")]
        public async Task<ActionResult<OrderRequestsDto>> createOrderRequest([FromRoute] long RestaurantId,
                         [FromBody] CreateOrderRequestDto createOrderRequestDto)
        {
            CreateOrderRequest createOrderRequest = _mapper.Map<CreateOrderRequest>(createOrderRequestDto);
            OrderRequestsDto orderRequestsDto = await consumerService.createOrderRequest(RestaurantId,
                                createOrderRequest);
                return Ok(orderRequestsDto);
        }

         [HttpGet("list/get-available-restaurant")]
        public async Task<ActionResult<Page<Restaurant>>> viewAvailableRestaurant() {
            PageRequest page = new PageRequest();
         Page<Restaurant> restaurants = await consumerService.getAllRestaurant(page);
                return Ok(restaurants);
        }


        [HttpGet("view-menu/{RestaurantId}")]
        public async Task<ActionResult<Menu>> viewMenu([FromRoute] long RestaurantId)
        {
           var menu = await consumerService.viewMenuByRestaurantId(RestaurantId);
            return Ok(menu);
        }

        [HttpGet("view-cart/{RestaurantId}")]
        public async Task<ActionResult<CartDto>> viewCart([FromRoute] long RestaurantId)
        {
           var CartDto = await consumerService.viewCart(RestaurantId);
            return Ok(CartDto);
        }


        [HttpGet("get-consumer-otp/{OrderId}")]
        public async Task<ActionResult<ConsumerOTP>> getConsumerOtp([FromRoute] long OrderId)
        {
          var OTP = await consumerService.getOtpByOrderId(OrderId);
            return Ok(OTP);
        }





    }
}
