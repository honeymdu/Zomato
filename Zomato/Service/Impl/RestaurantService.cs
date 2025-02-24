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

        public Restaurant AddNewRestaurant(RestaurantPartner restaurantPartner, RestaurantDto restaurantDto)
        {
            Restaurant restaurant = _mapper.Map<Restaurant>(restaurantDto);
            restaurant.isVarified = false;
            restaurant.isAvailable = true;
            restaurant.rating = 0.0;
            restaurant.restaurantPartner = restaurantPartner;
            _context.Restaurant.Add(restaurant);
            _context.SaveChanges();
            return restaurant;
        }

        public async Task<List<Restaurant>> GetAllVerifiedAndActiveRestaurants()
        {
            return await _context.Restaurant
                .Where(r => r.isAvailable && r.isVarified) // Use PascalCase for properties
                .OrderBy(r => r.id) // PascalCase for Id
                .ToListAsync(); // Use ToListAsync() for async execution
        }


        public IPagedList<Restaurant> getAllVarifiedRestaurant(int pageNumber, int pageSize)
        {
            return _context.Restaurant
            .Where(r => r.isAvailable && r.isVarified) // Filter available & verified restaurants
            .OrderBy(r => r.id) // Optional ordering
            .ToPagedList(pageNumber, pageSize); // Pagination
        }

        public Restaurant getRestaurantById(long restaurantId)
        {
            return _context.Restaurant.Find(restaurantId) ?? throw new ResourceNotFoundException("Restaurant Not Found with Id" + restaurantId);
        }

        public List<Restaurant> getRestaurantByRestaurantPartner(RestaurantPartner restaurantPartner)
        {
            return _context.Restaurant.Where(r => r.restaurantPartner.id == restaurantPartner.id).ToList();
        }

        public List<Restaurant> getTopTenNearestRestaurants(Point UserSrc)
        {
            return _context.Restaurant
            .Where(r => r.isAvailable && r.isVarified &&
                        r.restaurantLocation.IsWithinDistance(UserSrc, 15000)) // 15km radius
            .OrderBy(r => r.restaurantLocation.Distance(UserSrc)) // Sort by nearest
            .Take(10) // Limit 10
            .ToList();
        }

        public void IsRestaurentActiveOrVarified(long restaurantId)
        {
            if (!(_context.Restaurant.Any(r=>r.id == restaurantId)))
            {
                throw new ResourceNotFoundException("Restaurant Not Exist with Id =" + restaurantId);
            }

            if (!(_context.Restaurant.Any(r => r.id == restaurantId && r.isAvailable == true && r.isVarified == true)))
            {
                throw new Exception("Restaurant is not available for orders at the moment with Id =" + restaurantId);
            }
        }

        public bool IsRestaurentAlreadyExist(Restaurant newRestaurant)
        {
            return _context.Restaurant.Any(r => r.name == newRestaurant.name && r.restaurantPartner == newRestaurant.restaurantPartner); 
           
        }

        public Restaurant save(Restaurant restaurant)
        {
            _context.Update(restaurant);
            _context.SaveChanges();
            return restaurant;
        }

        public Menu viewMenu(long restaurantId)
        {
            return menuService.getMenuByRestaurant(restaurantId);
        }

        public Restaurant viewProfile(long restaurantId)
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
