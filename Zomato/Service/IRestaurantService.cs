using Azure;
using NetTopologySuite.Geometries;
using System.Runtime.InteropServices;
using X.PagedList;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Service
{
    public interface IRestaurantService
    {

        Task<Restaurant> AddNewRestaurant(RestaurantPartner restaurantPartner, RestaurantDto restaurantDto);

        Task<Restaurant> getRestaurantById(long restaurantId);

        Task<Restaurant> getRestaurantByIdAsync(long restaurantId);

        Task<Menu> viewMenu(long restaurantId);

        Task<Restaurant> viewProfile(long restaurantId);

//        IPagedList<Restaurant> findAllRestaurant(int pageNumber, int pageSize)

        Task<List<Restaurant>> getRestaurantByRestaurantPartner(RestaurantPartner restaurantPartner);

        Task<Boolean> IsRestaurentAlreadyExist(Restaurant newRestaurant);

        Task<Restaurant> save(Restaurant restaurant);

        Task<IPagedList<Restaurant>> getAllVarifiedRestaurant(int pageNumber, int pageSize);

        Task<List<Restaurant>> GetAllVerifiedAndActiveRestaurants();

        Task<List<Restaurant>> getTopTenNearestRestaurants(Point UserSrc);

        Task IsRestaurentActiveOrVarified(long restaurantId);

    }
}
