using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zomato.Dto;
using Zomato.Entity;
using Zomato.Entity.Enum;
using Zomato.Service.Impl;

namespace Zomato.Controllers
{
    [Route("api/delivery/partner")]
    [ApiController]
    [Authorize(Roles ="DELIVERY_PARTNER")]
    public class DeliveryPartnerController: ControllerBase
    {
        private readonly DeliveryPartnerService deliveryPartnerService;

        public DeliveryPartnerController(DeliveryPartnerService deliveryPartnerService)
        {
            this.deliveryPartnerService = deliveryPartnerService;
        }

        [HttpPost("/pick-up-order/{deliveryRequestId}")]
        public async Task<ActionResult<Boolean>> pickOrderFromRestaurant([FromRoute] long deliveryRequestId,
            [FromBody] RestaurantOTP restaurantOTP)
        {
           await deliveryPartnerService.pickupOrderFromRestaurant(deliveryRequestId, restaurantOTP.restaurantOTP);
            return Ok(true);
        }


         [HttpPost("/accept-delivery-request/{deliveryRequestId}")]
        public async Task<ActionResult<Boolean>> acceptdeliveryRequest([FromRoute] long deliveryRequestId)
        {
           await deliveryPartnerService.acceptDeliveryRequest(deliveryRequestId);
            return Ok(true);
        }

        [HttpPost("/cancel-delivery-request/{deliveryRequestId}")]
        public async Task<ActionResult<Boolean>> canceldeliveryRequest([FromRoute] long deliveryRequestId)
        {
            await deliveryPartnerService.cancelDeliveryRequest(deliveryRequestId);
            return Ok(true);
        }


         [HttpPost("/complete-delivery-request/{deliveryRequestId}")]
        public async Task<ActionResult<Boolean>> completedeliveryRequest([FromRoute] long deliveryRequestId,
            [FromBody] ConsumerOTP consumerOTP)
        {
           await deliveryPartnerService.completeOrderDelivery(deliveryRequestId, consumerOTP.consumerOTP);
            return Ok(true);
        }


    }
}
