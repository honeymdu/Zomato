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

        Restaurant AddNewRestaurant(RestaurantPartner restaurantPartner, RestaurantDto restaurantDto);

        Restaurant getRestaurantById(long restaurantId);

        Task<Restaurant> getRestaurantByIdAsync(long restaurantId);

        Menu viewMenu(long restaurantId);

        Restaurant viewProfile(long restaurantId);

//        IPagedList<Restaurant> findAllRestaurant(int pageNumber, int pageSize)

        List<Restaurant> getRestaurantByRestaurantPartner(RestaurantPartner restaurantPartner);

        Boolean IsRestaurentAlreadyExist(Restaurant newRestaurant);

        Restaurant save(Restaurant restaurant);

        IPagedList<Restaurant> getAllVarifiedRestaurant(int pageNumber, int pageSize);

        Task<List<Restaurant>> GetAllVerifiedAndActiveRestaurants();

        List<Restaurant> getTopTenNearestRestaurants(Point UserSrc);

        void IsRestaurentActiveOrVarified(long restaurantId);

    }
}
