using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Zomato.Dto;
using Zomato.Entity;
using Zomato.Service;
using Zomato.Service.Impl;

namespace Zomato.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class AdminController : ControllerBase
    {

        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("onBoard-Restaurant-Partner/{userId}")]
        public async Task<ActionResult<RestaurantPartnerDto>> OnBoardNewRestaurantPartner([FromRoute] long userId, [FromBody] OnBoardRestaurantPartnerDto onBoardRestaurantPartnerDto)
        {
            var restaurantPartner =  await _adminService.onBoardNewRestaurantPartner(userId, onBoardRestaurantPartnerDto);
            return Ok(restaurantPartner);
        }

        [HttpPost("onBoard-Delivery-Partner/{UserId}")]
        public async Task<ActionResult<DeliveryPartnerDto>> onBoardNewDeliveryPartner([FromRoute] long UserId,
                [FromBody] OnBoardDeliveryPartnerDto onBoardDeliveryPartnerDto)
        {
          var deliveryPartnerDto =  await _adminService.onBoardDeliveryPartner(UserId, onBoardDeliveryPartnerDto);
            return Ok(deliveryPartnerDto);
        }


        [HttpGet("list/get-all-restaurant")]
        public async Task<ActionResult<Page<RestaurantDto>>> GetAllRestaurant()
        {
            var pageRequest = new PageRequest();
            var result = await _adminService.getAllRestaurant(pageRequest);
            return Ok(result);
        }

        [HttpGet("list/get-all-delivery-partner")]
        public async Task<ActionResult<Page<DeliveryPartnerDto>>> GetAllDeliveryPartner()
        {
            var pageRequest = new PageRequest();
            var result = await _adminService.getAllDeliveryPartner(pageRequest);
            return Ok(result);
        }



        [HttpPost("verify-Restaurant/{RestaurantId}")]
        public async Task<ActionResult<bool>> VerifyRestaurant([FromRoute] long RestaurantId)
        {
            bool isVerified = await _adminService.varifyRestaurant(RestaurantId);
            return Ok(isVerified);
        }

    }

}
