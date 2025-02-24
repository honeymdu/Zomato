using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Strategies.Impl
{
    public class DeliveryPartnerMatchingHighestRatingDeliveryPartnerStartegy : IDeliveryPartnerMatchingStrategy
    {
        private readonly AppDbContext _context;

        public DeliveryPartnerMatchingHighestRatingDeliveryPartnerStartegy(AppDbContext context)
        {
            _context = context;
        }

        public List<DeliveryPartner> findMatchingDeliveryPartner(DeliveryFareGetDto deliveryFareGetDto)
        {
           return _context.DeliveryPartner.FromSqlRaw(@"SELECT TOP 10 d.* 
                      FROM delivery_partner d 
                      WHERE d.available = 1 
                      AND d.current_location.STDistance({0}) <= 15000 
                      ORDER BY d.rating DESC", deliveryFareGetDto.PickupLocation)
          .ToList();
        }
    }
}
