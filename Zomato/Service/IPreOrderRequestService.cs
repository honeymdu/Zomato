using System.Runtime.InteropServices;
using NetTopologySuite.Geometries;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Service
{
    public interface IPreOrderRequestService
    {

        Double calculateTotalPrice(PreOrderRequestDto preOrderRequestDto);

        // Function to validate the preorder request (e.g., check if restaurant is
        // active, if user exists)
        void validatePreOrderRequest(long restaurantId, long ConsumerId, Cart cart);

        PreOrderRequestDto createPreOrderRequest(Cart cart, Point UserLocation);

        Boolean isDeliveryAddressValid(Point UserLocation);

        Double calculateDeliverPrice(Cart cart, Point UserLocation);

    }
}
