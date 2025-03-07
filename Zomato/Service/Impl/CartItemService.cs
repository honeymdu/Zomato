using AutoMapper;
using Zomato.Data;
using Zomato.Exceptions.CustomExceptionHandler;
using Zomato.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Zomato.Service.Impl
{
    public class CartItemService : ICartItemService
    {
        private readonly AppDbContext _context;

        public CartItemService(AppDbContext context)
        {
            _context = context;
        }

        public CartItem createNewCartItem(MenuItem menuItem, Cart cart)
        {
            var cartItem = new CartItem
            {
                menuItem = menuItem,
                cart = cart,
                quantity = 1,
                totalPrice = menuItem.price
            };
            return cartItem;
        }

        public void decrementCartItemQuantity(int quantity, CartItem cartItem)
        {
            if (quantity <= 0)
            {
                throw new IllegalArgumentException("Quantity must be greater than zero.");
            }
            if (cartItem.quantity < quantity)
            {
                throw new IllegalArgumentException("Quantity to decrement exceeds current quantity.");
            }

            cartItem.quantity = cartItem.quantity - quantity;
            cartItem.totalPrice = cartItem.menuItem.price * cartItem.quantity;
            cartItem.cart.totalPrice = cartItem.cart.totalPrice
                    - (cartItem.menuItem.price * quantity);
        }

        public async Task<List<CartItem>> getAllCartItemsByCartId(long cartId)
        {
            return await _context.CartItem.Where(c => c.cart.id.Equals(cartId)).ToListAsync();
        }

        public async Task<CartItem> getCartItemById(long cartItemId)
        {
            return await _context.CartItem.FindAsync(cartItemId)?? 
                throw new ResourceNotFoundException("CartItem not found for the given menu item in cart.");
        }

        public CartItem getCartItemByMenuItemAndCart(MenuItem menuItem, Cart cart)
        {
            return cart.cartItems
            .FirstOrDefault(cartItem => cartItem.menuItem.id == menuItem.id)
            ?? throw new ResourceNotFoundException("CartItem not found for the given menu item in cart.");
        }

        public void incrementCartItemQuantity(int quantity, CartItem cartItem)
        {
            if (quantity == 0 || quantity < 0)
            {
                throw new Exception("Quantity has to be greater than zero");
            }
            cartItem.quantity = cartItem.quantity + quantity;
            cartItem.totalPrice = cartItem.menuItem.price * cartItem.quantity;
        }

        public async Task<bool> isCartItemExist(CartItem cartItem)
        {
            return await _context.CartItem.AnyAsync(c=>c.id.Equals(cartItem.id));
        }

        public bool isMenuItemExistInCart(MenuItem menuItem, Cart cart)
        {
            return cart.cartItems.Any(c => c.menuItem.id == menuItem.id && c.cart.id == cart.id);
        }

        public void removeCartItemFromCart(CartItem cartItem)
        {
            var Cart = cartItem.cart;
            Cart.cartItems.Remove(cartItem);
            Cart.totalPrice = Cart.totalPrice - cartItem.totalPrice;        }
    }
}
