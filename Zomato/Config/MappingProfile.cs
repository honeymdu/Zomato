using AutoMapper;
using NetTopologySuite.Geometries;
using Zomato.Dto;
using Zomato.Entity;
using Zomato.Util;

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
            CreateMap<OrderRequests, OrderRequestsDto>();















            // String to Long? conversion
            CreateMap<string, long?>().ConvertUsing<StringToLongConverter>();
            // PointDto to Point conversion
            CreateMap<PointDto, Point>()
                .ConvertUsing(src => GeometryUtil.CreatePoint(src));

            // Point to PointDto conversion
            CreateMap<Point, PointDto>()
                .ConvertUsing(src => new PointDto(new double[] { src.X, src.Y }));
        }
    }


    public class StringToLongConverter : ITypeConverter<string, long?>
    {
        public long? Convert(string source, long? destination, ResolutionContext context)
        {
            if (long.TryParse(source, out long result))
            {
                return result;
            }
            return null; // Return null if parsing fails
        }
    }
}

