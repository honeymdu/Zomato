using AutoMapper;
using Azure;
using NetTopologySuite.Geometries;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Exceptions.CustomExceptionHandler;
using X.PagedList;
using X.PagedList.Extensions;
using Zomato.Entity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Zomato.Service.Impl
{
    public class RestaurantService : IRestaurantService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMenuService menuService;

        public RestaurantService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Restaurant> AddNewRestaurant(RestaurantPartner restaurantPartner, RestaurantDto restaurantDto)
        {
            Restaurant restaurant = _mapper.Map<Restaurant>(restaurantDto);
            restaurant.isVarified = false;
            restaurant.isAvailable = true;
            restaurant.rating = 0.0;
            restaurant.restaurantPartner = restaurantPartner;
            var updateRestaurant = _context.Restaurant.Add(restaurant).Entity;
            await _context.SaveChangesAsync();
            return updateRestaurant;
        }

        public async Task<List<Restaurant>> GetAllVerifiedAndActiveRestaurants()
        {
            return await _context.Restaurant
                .Where(r => r.isAvailable && r.isVarified) 
                .OrderBy(r => r.id) 
                .ToListAsync();
        }


        public async Task<IPagedList<Restaurant>> getAllVarifiedRestaurant(int pageNumber, int pageSize)
        {
            var query = _context.Restaurant
            .Where(r => r.isAvailable && r.isVarified)
            .OrderBy(r => r.id);

            // Get paginated list manually
            var restaurants = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            // Convert to PagedList
            return new StaticPagedList<Restaurant>(restaurants, pageNumber, pageSize, await query.CountAsync());
        }

        public async Task<Restaurant> getRestaurantById(long restaurantId)
        {
           var restaurant = await _context.Restaurant.FindAsync(restaurantId);
            if(restaurant == null)
            {
                throw new ResourceNotFoundException("Restaurant Not Found with Id" + restaurantId);
            }
            return restaurant;

        }

        public async Task<List<Restaurant>> getRestaurantByRestaurantPartner(RestaurantPartner restaurantPartner)
        {
            return await _context.Restaurant.Where(r => r.restaurantPartner.id == restaurantPartner.id).ToListAsync();
        }

        public async Task<List<Restaurant>> getTopTenNearestRestaurants(Point UserSrc)
        {
            return await _context.Restaurant
            .Where(r => r.isAvailable && r.isVarified &&
                        r.restaurantLocation.IsWithinDistance(UserSrc, 15000)) 
            .OrderBy(r => r.restaurantLocation.Distance(UserSrc)) 
            .Take(10)
            .ToListAsync();
        }

        public async Task IsRestaurentActiveOrVarified(long restaurantId)
        {
            if (!(await _context.Restaurant.AnyAsync(r=>r.id == restaurantId)))
            {
                throw new ResourceNotFoundException("Restaurant Not Exist with Id =" + restaurantId);
            }

            if (!(await _context.Restaurant.AnyAsync(r => r.id == restaurantId && r.isAvailable == true && r.isVarified == true)))
            {
                throw new Exception("Restaurant is not available for orders at the moment with Id =" + restaurantId);
            }
        }

        public async Task<bool> IsRestaurentAlreadyExist(Restaurant newRestaurant)
        {
            return await _context.Restaurant.AnyAsync(r => r.name == newRestaurant.name && r.restaurantPartner == newRestaurant.restaurantPartner); 
           
        }

        public async Task<Restaurant> save(Restaurant restaurant)
        {
           var updatedRestaurant = _context.Update(restaurant).Entity;
            await _context.SaveChangesAsync();
            return updatedRestaurant;
        }

        public async Task<Menu> viewMenu(long restaurantId)
        {
            return await menuService.getMenuByRestaurant(restaurantId);
        }

        public async Task<Restaurant> viewProfile(long restaurantId)
        {
            throw new NotImplementedException();
        }

        public async Task<Restaurant> getRestaurantByIdAsync(long restaurantId)
        {
            var restaurant = await _context.Restaurant.FindAsync(restaurantId);
            return restaurant ?? throw new ResourceNotFoundException("Restaurant Not Found with Id" + restaurantId);
        }

    }
}
