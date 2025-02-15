using Azure;
using NetTopologySuite.Geometries;
using System.Runtime.InteropServices;
using Zomato.Dto;
using Zomato.Model;

namespace Zomato.Service
{
    public interface IRestaurantService
    {

        Restaurant AddNewRestaurant(RestaurantPartner restaurantPartner, RestaurantDto restaurantDto);

        Restaurant getRestaurantById(long restaurantId);

        Menu viewMenu(long restaurantId);

        Restaurant viewProfile(long restaurantId);

        //Page<Restaurant> findAllRestaurant(Pageable pageRequest);

        List<Restaurant> getRestaurantByRestaurantPartner(RestaurantPartner restaurantPartner);

        Boolean IsRestaurentAlreadyExist(Restaurant newRestaurant);

        Restaurant save(Restaurant restaurant);

        Page<Restaurant> getAllVarifiedRestaurant(PageRequest pageRequest);

        List<Restaurant> getAllVarifiedAndActiveRestaurant();

        List<Restaurant> getTopTenNearestRestaurants(Point UserSrc);

        void IsRestaurentActiveOrVarified(long restaurantId);

    }
}
