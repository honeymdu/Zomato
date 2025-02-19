using System.Collections;
using AutoMapper;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Exceptions.CustomExceptionHandler;
using Zomato.Model;

namespace Zomato.Service.Impl
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IRestaurantService restaurantService;
        private readonly IMapper _mapper;

        public CartService(AppDbContext context, IConfiguration config, IRestaurantService restaurantService, IMapper mapper)
        {
            _context = context;
            _config = config;
            this.restaurantService = restaurantService;
            _mapper = mapper;
        }

        public CartDto addItemToCart(long CartId, CartItem cartItem)
        {
            var cart = _context.Cart.Find(CartId);
            isValidCart(cart);
            if (cart.cartItems == null)
            {
                cart.cartItems = new List<CartItem>();
            }
            cart.cartItems.Add(cartItem);
            cart.totalPrice = cart.totalPrice
                + (cartItem.menuItem.price * cartItem.totalPrice);
            _context.Cart.Update(cart);
            return _mapper.Map<CartDto>(cart);

        }

        public Cart clearCartItemFromCart(long CartId)
        {
            var cart = _context.Cart.Find(CartId);
            isValidCart(cart);
            cart.cartItems.Clear();
            cart.totalPrice = 0.0;
            return _context.Cart.Update(cart).Entity;
        }

        public Cart createCart(long restaurantId, Consumer consumer)
        {
            Restaurant restaurant = restaurantService.getRestaurantById(restaurantId);
            var cart = new Cart() 
            {   restaurant = restaurant,
                consumer = consumer, 
                totalPrice = 0.0,
                validCart = true
            };
            _context.Cart.Add(cart);
            _context.SaveChanges();
            return cart;
        }

        public void deleteAllCartItemByCartId(long cartId)
        {
            var cart = _context.Cart.Find(cartId);
            clearCartItemFromCart(cartId);

            inValidCart(cart);
        }

        public Cart getCartByConsumerIdAndRestaurantId(long ConsumerId, long restaurantId)
        {
            throw new NotImplementedException();
        }

        public Cart getCartById(long CartId)
        {
           return _context.Cart.Find(CartId) ?? throw new ResourceNotFoundException("Cart Not found with cart Id = "+CartId);
        }

        public void inValidCart(Cart cart)
        {
            throw new NotImplementedException();
        }

        public void isValidCart(Cart cart)
        {
            throw new NotImplementedException();
        }

        public bool isValidCartExist(Consumer consumer, long RestaurantId)
        {
            throw new NotImplementedException();
        }

        public CartDto prepareCart(Consumer consumer, long RestaurantId, long MenuItemId)
        {
            throw new NotImplementedException();
        }

        public CartDto removeItemFromCart(long CartId, CartItem cartItem)
        {
            throw new NotImplementedException();
        }

        public Cart saveCart(Cart cart)
        {
           return _context.Cart.Update(cart).Entity;
        }

        public CartDto viewCart(long CartId)
        {
            throw new NotImplementedException();
        }
    }
}
