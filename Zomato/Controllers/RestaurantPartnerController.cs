using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Index.HPRtree;
using Zomato.Dto;
using Zomato.Entity;
using Zomato.Entity.Enum;
using Zomato.Service;
using Zomato.Service.Impl;

namespace Zomato.Controllers
{
    [Route("api/restaurant-partner")]
    [ApiController]
    [Authorize(Roles = "RESTAURENT_PARTNER")]
    public class RestaurantPartnerController : ControllerBase
    {

        private readonly IRestaurantPartnerService _restaurantPartnerService;
        private readonly IMapper _mapper;

        [HttpPost("add-my-restaurant")]
        public async Task<ActionResult<RestaurantDto>> AddMyRestaurant([FromBody] AddNewRestaurantDto addNewRestaurantDto)
        {
            var restaurantPartnerDto = await _restaurantPartnerService.createRestaurant(addNewRestaurantDto);

            return CreatedAtAction(nameof(AddMyRestaurant), restaurantPartnerDto);
        }


        [HttpPost("add-my-restaurant-menu-item/{RestaurantId}")]
        public async Task<ActionResult<Menu>> AddMenuItemToMenu([FromRoute] long RestaurantId,
            [FromBody] MenuItemDto menuItemDto)
        {
            var menu = await _restaurantPartnerService.addMenuItemToMenu(menuItemDto, RestaurantId);
            return Ok(menu);

        }

        [HttpPost("create-restaurant-menu/{RestaurantId}")]
        public async Task<ActionResult<Menu>> CreateMenu([FromRoute] long RestaurantId,
            [FromBody] CreateMenu createMenu)
        {
            Restaurant restaurant = await _restaurantPartnerService.ViewMyRestaurantProfile(RestaurantId);
            createMenu.restaurant = restaurant;
            var menu = await _restaurantPartnerService.CreateMenu(createMenu);
            return CreatedAtAction(nameof(CreateMenu),menu);

        }


         [HttpPost("update-order-status/{OrderId}")]    
        public async Task<ActionResult<OrderDto>> updateOrderStatus([FromRoute] long OrderId,
            [FromBody] SetOrderStatusDto setOrderStatusDto)
        {
            Order order = await _restaurantPartnerService.updateOrderStatus(OrderId, setOrderStatusDto.orderStatus);

            return Ok(order);
            
        }

        [HttpPost("accept-order-Request/{OrderRequestId}")]
        public async Task<ActionResult<OrderDto>> acceptOrderRequest([FromRoute] long OrderRequestId)
        {
            Order order = await _restaurantPartnerService.acceptOrderRequest(OrderRequestId);
            var OrderDto = _mapper.Map<OrderDto>(order);
            return CreatedAtAction(nameof(acceptOrderRequest), OrderDto);
        }

         [HttpGet("view-menu/{RestaurantId}")]
        public async Task<ActionResult<Menu>> viewMenu([FromRoute] long RestaurantId)
        {
            Menu menu = await _restaurantPartnerService.viewMenuByRestaurantId(RestaurantId);
            return Ok(menu);
        }


        [HttpGet("view-Order-Requests/{RestaurantId}")]
        public async Task<ActionResult<OrderRequestsDto>> viewOrderRequests([FromRoute] long RestaurantId)
        {
            List<OrderRequests> orderRequests = await _restaurantPartnerService
                    .viewOrderRequestsByRestaurantId(RestaurantId);
            var orderRequestDto = _mapper.Map<List<OrderRequestsDto>>(orderRequests);

            return Ok(orderRequestDto);
        
        }


        [HttpGet("check-my-Restaurant-active-status/{RestaurantId}")]
        public  async Task<ActionResult<Boolean>> checkRestaurantStatus([FromRoute] long RestaurantId)
        {
            Restaurant restaurant = await _restaurantPartnerService.ViewMyRestaurantProfile(RestaurantId);
            return Ok(restaurant.isAvailable);
        }


        [HttpGet("view-my-restaurant-profile/{RestaurantId}")]
        public async Task<ActionResult<Restaurant>> ViewMyRestaurantProfile([FromRoute] long RestaurantId)
        {

            Restaurant restaurant = await _restaurantPartnerService.ViewMyRestaurantProfile(RestaurantId);
            return Ok(restaurant);

        }

        [HttpGet("get-Restaurant-otp/{OrderId}")]
        public async Task<ActionResult<RestaurantOTP>> getConsumerOtp([FromRoute] long OrderId)
        {
            return Ok(await _restaurantPartnerService.getRestaurantOTPByOrderId(OrderId));
        }

    }
}

