using System.Runtime.InteropServices;
using Zomato.Dto;
using Zomato.Entity;

namespace Zomato.Service
{
    public interface ICartService
    {
        Cart createCart(long restaurantId, Consumer consumer);
        CartDto addItemToCart(long CartId, CartItem cartItem);
        CartDto viewCart(long CartId);
        CartDto removeItemFromCart(long CartId, CartItem cartItem);
        void isValidCart(Cart cart);
        Boolean isValidCartExist(Consumer consumer, long RestaurantId);
        void inValidCart(Cart cart);
        Cart getCartById(long CartId);
        Cart saveCart(Cart cart);
        Cart getCartByConsumerIdAndRestaurantId(long ConsumerId,long restaurantId);
        void deleteAllCartItemByCartId(long cartId);
        CartDto prepareCart(Consumer consumer, long RestaurantId, long MenuItemId);
        Cart clearCartItemFromCart(long CartId);
    }
}
