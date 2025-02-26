using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Strategies.Impl
{
    public class DeliveryPartnerMatchingNearestDeliveryPartnerStartegy : IDeliveryPartnerMatchingStrategy
    {
        private readonly AppDbContext _context;

        public DeliveryPartnerMatchingNearestDeliveryPartnerStartegy(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DeliveryPartner>> findMatchingDeliveryPartner(DeliveryFareGetDto deliveryFareGetDto)
        {
            return await _context.DeliveryPartner
                .FromSqlRaw(@"SELECT TOP 10 d.* 
                      FROM delivery_partner d 
                      WHERE d.available = 1 
                      ORDER BY d.current_location.STDistance(@p0)", deliveryFareGetDto.PickupLocation)
                .ToListAsync();
        }
    }
}
