using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Entity;
using Zomato.Entity.Enum;
using Zomato.Exceptions.CustomExceptionHandler;

namespace Zomato.Service.Impl
{
    public class DeliveryPartnerService : IDeliveryPartnerService
    {

        private readonly AppDbContext _context;
        private readonly UserContextService userContextService;
        private readonly IPaymentService paymentService;
        private readonly IDistanceService distanceService;
        private readonly IMapper _mapper;
        public DeliveryPartnerService(AppDbContext context, UserContextService userContextService,IPaymentService paymentService,IDistanceService distance,IMapper mapper1)
        {
            _context = context;
            this.userContextService = userContextService;
            this.paymentService = paymentService;
            this.distanceService = distance;
            _mapper = mapper1;
        }

        public async Task acceptDeliveryRequest(long deliveryRequestId)
        {
            DeliveryRequest deliveryRequest = await _context.DeliveryRequest.FindAsync(deliveryRequestId)??
                throw new ResourceNotFoundException ("DeliveryRequest Not Found =" + deliveryRequestId);

            var currentDeliveryPartner = await getCurrentDeliveryPartner();
            if (!currentDeliveryPartner.available == true)
            {
                throw new Exception("Can not accept delivery Request as Delivery Partner Not Available");
            }
            if (!deliveryRequest.deliveryRequestStatus.Equals(DeliveryRequestStatus.ACCEPTED))
            {
                throw new Exception("Can not accept delivery Request as Delivery Request Status ="
                        + deliveryRequest.deliveryRequestStatus);
            }

            deliveryRequest.deliveryRequestStatus = DeliveryRequestStatus.ACCEPTED;
            currentDeliveryPartner.available =false;
            deliveryRequest.order.orderStatus = OrderStatus.DELIVERY_PARTNER_ASSIGNED;
            deliveryRequest.deliveryPartner = currentDeliveryPartner;
            _context.DeliveryRequest.Add(deliveryRequest);
            await  _context.SaveChangesAsync();
        }

        public async Task cancelDeliveryRequest(long deliveryRequestId)
        {
            DeliveryRequest deliveryRequest = await _context.DeliveryRequest.FindAsync(deliveryRequestId) ??
                throw new ResourceNotFoundException("DeliveryRequest Not Found =" + deliveryRequestId);

            if (deliveryRequest.deliveryRequestStatus.Equals(DeliveryRequestStatus.ACCEPTED))
            {
                throw new Exception("Can not cancel delivery Request as Delivery Request Status ="
                        + deliveryRequest.deliveryRequestStatus);
            }
            deliveryRequest.deliveryRequestStatus = DeliveryRequestStatus.PENDING;
            deliveryRequest.order.orderStatus = OrderStatus.DELIVERY_REQUEST_CREATED;
            var currentDeliveryPartner = await getCurrentDeliveryPartner();
            currentDeliveryPartner.available = true;
            deliveryRequest.deliveryPartner = currentDeliveryPartner;
            _context.DeliveryRequest.Update(deliveryRequest);
            await _context.SaveChangesAsync();
        }

        public async Task completeOrderDelivery(long deliveryRequestId, string consumerOtp)
        {
            DeliveryRequest deliveryRequest = await _context.DeliveryRequest.FindAsync(deliveryRequestId) ??
               throw new ResourceNotFoundException("DeliveryRequest Not Found =" + deliveryRequestId);
            if (!deliveryRequest.deliveryRequestStatus.Equals(DeliveryRequestStatus.ACCEPTED)
                    && deliveryRequest.deliveryPartner.Equals(getCurrentDeliveryPartner()))
            {
                throw new Exception(
                        "Can not Complete the order as this Delivery Request is assosiated with current partner, DeliverRequestStatus = "
                                + deliveryRequest.deliveryRequestStatus);
            }

            if (!deliveryRequest.consumerOtp.Equals(consumerOtp))
            {
                throw new Exception("OTP NOT ACCEPTED");
            }
            deliveryRequest.deliveryRequestStatus =DeliveryRequestStatus.COMPLETED;
            deliveryRequest.order.orderStatus = OrderStatus.DELIVERED;
           await paymentService.processPayment(deliveryRequest.order);
            _context.DeliveryRequest.Update(deliveryRequest);
           await _context.SaveChangesAsync();
        }

        public Page<DeliveryPartner> getAllDeliveryPartner(PageRequest pageRequest)
        {
            throw new NotImplementedException();
        }

        public async Task<DeliveryPartner> getCurrentDeliveryPartner()
        {
            var userEmail = userContextService.GetUserEmail();
            var user = await _context.User.Where(u => u.email == userEmail).FirstOrDefaultAsync();
            return await _context.DeliveryPartner.Where(rp => rp.user == user).FirstOrDefaultAsync() ?? throw new ResourceNotFoundException("Current User not Found");
        }

        public async Task<Point> getCurrentLocation()
        {
            var current = await getCurrentDeliveryPartner();
            return current.currentLocation;
        }

        public async Task pickupOrderFromRestaurant(long deliveryRequestId, string restaurantOTP)
        {
            DeliveryRequest deliveryRequest = await _context.DeliveryRequest.FindAsync(deliveryRequestId) ??
               throw new ResourceNotFoundException("DeliveryRequest Not Found =" + deliveryRequestId);
            if (!deliveryRequest.deliveryRequestStatus.Equals(DeliveryRequestStatus.ACCEPTED)
                    && deliveryRequest.deliveryPartner.Equals(getCurrentDeliveryPartner()))
            {
                throw new Exception(
                        "Can not Picked the order as this Delivery Request is assosiated with current partner, DeliverRequestStatus = "
                                + deliveryRequest.deliveryRequestStatus);
            }

            if (!deliveryRequest.restaurantOtp.Equals(restaurantOTP))
            {
                throw new Exception("OTP NOT ACCEPTED");
            }
            deliveryRequest.order.orderStatus = OrderStatus.OUT_FOR_DELIVERY;
            long distance = (long) await distanceService.CalculateDistance(deliveryRequest.PickupLocation,
                    deliveryRequest.DropLocation);
            deliveryRequest.deliveryTime =DateTime.Now.AddMinutes(10 * distance);
            _context.DeliveryRequest.Update(deliveryRequest);
            await _context.SaveChangesAsync();
        }

        public async Task rateDeliveryPartner(long UserId, double rating)
        {
            DeliveryPartner deliveryPartner = await _context.DeliveryPartner.Where(u => u.id == UserId).FirstOrDefaultAsync() ?? throw new ResourceNotFoundException("Delivery Partner not found");
            deliveryPartner.rating =deliveryPartner.rating + rating;
            await save(deliveryPartner);
        }

        public async Task<DeliveryPartnerDto> save(DeliveryPartner deliveryPartner)
        {
           var delivery_partner = _context.DeliveryPartner.Update(deliveryPartner);
            await _context.SaveChangesAsync();
            return _mapper.Map<DeliveryPartnerDto>(delivery_partner);
        }
    }
}
