using System.Runtime.InteropServices;
using Zomato.Entity;

namespace Zomato.Service
{
    public interface ICartItemService
    {
        Task<CartItem> getCartItemById(long cartItemId);

        CartItem createNewCartItem(MenuItem menuItem, Cart cart);

        void incrementCartItemQuantity(int quantity, CartItem cartItem);

        void decrementCartItemQuantity(int quantity, CartItem cartItem);

        void removeCartItemFromCart(CartItem cartItem);

        Task<Boolean> isCartItemExist(CartItem cartItem);

        CartItem getCartItemByMenuItemAndCart(MenuItem menuItem, Cart cart);

        Task<List<CartItem>> getAllCartItemsByCartId(long cartId);

        Boolean isMenuItemExistInCart(MenuItem menuItem, Cart cart);
    }
}
