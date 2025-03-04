using AutoMapper;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Config
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();  // Ensure this mapping exists
        }
    }
}
