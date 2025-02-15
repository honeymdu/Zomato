using System.Runtime.InteropServices;
using Zomato.Model;

namespace Zomato.Service
{
    public interface ICartItemService
    {
        CartItem getCartItemById(long cartItemId);

        CartItem createNewCartItem(MenuItem menuItem, Cart cart);

        void incrementCartItemQuantity(int quantity, CartItem cartItem);

        void decrementCartItemQuantity(int quantity, CartItem cartItem);

        void removeCartItemFromCart(CartItem cartItem);

        Boolean isCartItemExist(CartItem cartItem);

        CartItem getCartItemByMenuItemAndCart(MenuItem menuItem, Cart cart);

        List<CartItem> getAllCartItemsByCartId(long cartId);

        Boolean isMenuItemExistInCart(MenuItem menuItem, Cart cart);
    }
}
