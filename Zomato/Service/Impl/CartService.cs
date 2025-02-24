using System;
using System.Collections;
using AutoMapper;
using NetTopologySuite.Index.HPRtree;
using Zomato.Data;
using Zomato.Dto;
using Zomato.Entity;
using Zomato.Exceptions.CustomExceptionHandler;

namespace Zomato.Service.Impl
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IRestaurantService restaurantService;
        private readonly IMapper _mapper;
        private readonly IMenuService menuService;
        private readonly ICartItemService cartItemService;

        public CartService(AppDbContext context, IConfiguration config, IRestaurantService restaurantService, IMapper mapper,IMenuService menuService,ICartItemService cartItemService)
        {
            _context = context;
            _config = config;
            this.restaurantService = restaurantService;
            _mapper = mapper;
            this.menuService = menuService;
            this.cartItemService = cartItemService;
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
            return _context.Cart.Where(c => c.restaurant.id == restaurantId && c.consumer.id == ConsumerId && c.validCart == true)
                .SingleOrDefault()?? throw new ResourceNotFoundException("Cart Not found"); 
        
        }
            

        public Cart getCartById(long CartId)
        {
           return _context.Cart.Find(CartId) ?? throw new ResourceNotFoundException("Cart Not found with cart Id = "+CartId);
        }

        public void inValidCart(Cart cart)
        {
            cart.validCart = false;
            _context.Update(cart);
            _context.SaveChanges();
        }

        public void isValidCart(Cart cart)
        {
            if (!cart.validCart)
            {
                throw new InvalidCartException("Cart is not valid with cartId " + cart.id);
            }
            if (!restaurantService.getRestaurantById(cart.restaurant.id).isAvailable)
            {
                inValidCart(cart);
                throw new InvalidCartException("Cart is not valid with cartId " + cart.id);
            }
        }

        public bool isValidCartExist(Consumer consumer, long RestaurantId)
        {
            var cart = getCartByConsumerIdAndRestaurantId(consumer.id, RestaurantId);
            if (cart == null)
            {
                return false;
            }
            return true;
        }

        public CartDto prepareCart(Consumer consumer, long RestaurantId, long MenuItemId)
        {
            // check cart already exist with current Restaurent Id
            bool isValidCart = isValidCartExist(consumer, RestaurantId);

            if (!isValidCart)
            {
                var _cart = createCart(RestaurantId, consumer);
                var _menuItem = menuService.getMenuItemById(RestaurantId, MenuItemId);
                var _cartItem = cartItemService.createNewCartItem(_menuItem, _cart);
                return addItemToCart(_cart.id, _cartItem);
            }

            Cart cart = getCartByConsumerIdAndRestaurantId(consumer.id, RestaurantId);
            MenuItem menuItem = menuService.getMenuItemById(RestaurantId, MenuItemId);

            if (cartItemService.isMenuItemExistInCart(menuItem, cart))
            {
                CartItem cartItem = cartItemService.getCartItemByMenuItemAndCart(menuItem, cart);
                cartItemService.incrementCartItemQuantity(1, cartItem);
                var updatedCart = refreshCartTotalPrice(cart);
                CartDto cartDto = _mapper.Map<CartDto>(cart);
                return cartDto;
            }
            else
            {
                CartItem cartItem = cartItemService.createNewCartItem(menuItem, cart);
                return addItemToCart(cart.id, cartItem);
            }

            }
 
        public CartDto removeItemFromCart(long CartId, CartItem cartItem)
        {
            Cart cart = getCartById(CartId);
            isValidCart(cart);

            if (!cartItemService.isCartItemExist(cartItem))
            {
                throw new ResourceNotFoundException("CartItem not found in Cart with ID " + CartId);
            }

            if (cartItem.quantity > 1)
            {
                cartItemService.decrementCartItemQuantity(1, cartItem);
            }
            else
            {
                cartItemService.removeCartItemFromCart(cartItem);
            }
            refreshCartTotalPrice(cart);
            cart = getCartById(CartId);
            CartDto cartDto = _mapper.Map<CartDto>(cart);
            return cartDto;
        }

        public Cart saveCart(Cart cart)
        {
           return _context.Cart.Update(cart).Entity;
        }

        public CartDto viewCart(long CartId)
        {
            var cart = getCartById(CartId);
            CartDto cartDto = _mapper.Map<CartDto>(cart);
            List<CartItem> cartItem = cartItemService.getAllCartItemsByCartId(CartId);
            List<CartItemDto> cartItemdto = _mapper.Map<List<CartItemDto>>(cartItem);
            cartDto.cartItems = cartItemdto;
            return cartDto;

        }

        private Cart refreshCartTotalPrice(Cart cart)
        {
            cart.totalPrice = cart.cartItems.Sum(item => item.totalPrice);
            return _context.Cart.Update(cart).Entity;
        }
    }
}
