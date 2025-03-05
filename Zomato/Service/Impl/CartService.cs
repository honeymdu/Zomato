using System;
using System.Collections;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<CartDto> addItemToCart(long CartId, CartItem cartItem)
        {
            var cart = _context.Cart.Find(CartId);
            await isValidCart(cart);
            if (cart.cartItems == null)
            {
                cart.cartItems = new List<CartItem>();
            }
            cart.cartItems.Add(cartItem);
            cart.totalPrice = cart.totalPrice
                + (cartItem.menuItem.price * cartItem.totalPrice);
            _context.Cart.Update(cart);
            await _context.SaveChangesAsync();
            return _mapper.Map<CartDto>(cart);

        }

        public async Task<Cart> clearCartItemFromCart(long CartId)
        {
            var cart = _context.Cart.Find(CartId);
            await isValidCart(cart);
            cart.cartItems.Clear();
            cart.totalPrice = 0.0;
            _context.Cart.Update(cart);
           await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> createCart(long restaurantId, Consumer consumer)
        {
            var restaurant = await restaurantService.getRestaurantById(restaurantId);
            var cart = new Cart() 
            {   restaurant = restaurant,
                consumer = consumer, 
                totalPrice = 0.0,
                validCart = true
            };
            _context.Cart.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task deleteAllCartItemByCartId(long cartId)
        {
            var cart = await _context.Cart.FindAsync(cartId);
            await clearCartItemFromCart(cartId);

           await inValidCart(cart);
        }

        public async Task<Cart> getCartByConsumerIdAndRestaurantId(long ConsumerId, long restaurantId)
        {
            var cart = await _context.Cart.Where(c => c.restaurant.id == restaurantId && c.consumer.id == ConsumerId && c.validCart == true)
                .SingleOrDefaultAsync();
            if(cart == null)
            {
                throw new ResourceNotFoundException("Cart Not found");
            }

            return cart;    
        }
            

        public async Task<Cart> getCartById(long CartId)
        {
           return await _context.Cart.FindAsync(CartId) ?? throw new ResourceNotFoundException("Cart Not found with cart Id = "+CartId);
        }

        public async Task inValidCart(Cart cart)
        {
            cart.validCart = false;
            _context.Update(cart);
           await _context.SaveChangesAsync();
        }

        public async Task isValidCart(Cart cart)
        {
            if (!cart.validCart)
            {
                throw new InvalidCartException("Cart is not valid with cartId " + cart.id);
            }
            var restaurant = await restaurantService.getRestaurantById(cart.restaurant.id);
            if (!restaurant.isAvailable)
            {
                await inValidCart(cart);
                throw new InvalidCartException("Cart is not valid with cartId " + cart.id);
            }
        }

        public async Task<bool> isValidCartExist(Consumer consumer, long RestaurantId)
        {
            var cart = await getCartByConsumerIdAndRestaurantId(consumer.id, RestaurantId);
            if (cart == null)
            {
                return false;
            }
            return true;
        }

        public async Task<CartDto> prepareCart(Consumer consumer, long RestaurantId, long MenuItemId)
        {
            // check cart already exist with current Restaurent Id
            bool isValidCart = await isValidCartExist(consumer, RestaurantId);

            if (!isValidCart)
            {
                var _cart = await createCart(RestaurantId, consumer);
                var _menuItem = await menuService.getMenuItemById(RestaurantId, MenuItemId);
                var _cartItem = cartItemService.createNewCartItem(_menuItem, _cart);
                return await addItemToCart(_cart.id, _cartItem);
            }

            var cart = await getCartByConsumerIdAndRestaurantId(consumer.id, RestaurantId);
            MenuItem menuItem = await menuService.getMenuItemById(RestaurantId, MenuItemId);

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
                return await addItemToCart(cart.id, cartItem);
            }

        }
 
        public async Task<CartDto> removeItemFromCart(long CartId, CartItem cartItem)
        {
            var cart = await getCartById(CartId);
            await isValidCart(cart);

            if (!await cartItemService.isCartItemExist(cartItem))
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
            await refreshCartTotalPrice(cart);
            cart = await getCartById(CartId);
            CartDto cartDto = _mapper.Map<CartDto>(cart);
            return cartDto;
        }

        public async Task<Cart> saveCart(Cart cart)
        {
            _context.Cart.Update(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<CartDto> viewCart(long CartId)
        {
            var cart = getCartById(CartId);
            CartDto cartDto = _mapper.Map<CartDto>(cart);
            List<CartItem> cartItem =await cartItemService.getAllCartItemsByCartId(CartId);
            List<CartItemDto> cartItemdto = _mapper.Map<List<CartItemDto>>(cartItem);
            cartDto.cartItems = cartItemdto;
            return cartDto;

        }

        private async Task<Cart> refreshCartTotalPrice(Cart cart)
        {
            cart.totalPrice = cart.cartItems.Sum(item => item.totalPrice);
            _context.Cart.Update(cart);
           await _context.SaveChangesAsync();
            return cart;
        }
    }
}
