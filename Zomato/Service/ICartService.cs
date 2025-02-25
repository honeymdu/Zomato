using System.Runtime.InteropServices;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Service
{
    public interface ICartService
    {
        Task<Cart> createCart(long restaurantId, Consumer consumer);
        Task<CartDto> addItemToCart(long CartId, CartItem cartItem);
        Task<CartDto> viewCart(long CartId);
        Task<CartDto> removeItemFromCart(long CartId, CartItem cartItem);
        Task isValidCart(Cart cart);
        Task<Boolean> isValidCartExist(Consumer consumer, long RestaurantId);
        Task inValidCart(Cart cart);
        Task<Cart> getCartById(long CartId);
        Task<Cart> saveCart(Cart cart);
        Task<Cart> getCartByConsumerIdAndRestaurantId(long ConsumerId,long restaurantId);
        Task deleteAllCartItemByCartId(long cartId);
        Task<CartDto> prepareCart(Consumer consumer, long RestaurantId, long MenuItemId);
        Task<Cart> clearCartItemFromCart(long CartId);
    }
}
