using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Index.HPRtree;
using Zomato.Data;
using Zomato.Entity;
using Zomato.Entity.Enum;
using Zomato.Exceptions.CustomExceptionHandler;

namespace Zomato.Service.Impl
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            this._mapper = mapper;
        }

        public async Task<Order> cancelOrder(long OrderId)
        {
            Order order = await getOrderById(OrderId);
            if (order.orderStatus.Equals(OrderStatus.ACCEPTED))
            {
                order.orderStatus = OrderStatus.CANCELLED;
               var updatedOrder = _context.Order.Update(order).Entity;
               await _context.SaveChangesAsync();
                return updatedOrder;

            }
            throw new Exception("Can not cancel order as OrderRequest status is not Accepted earlier");

        }

        public async Task<Order> createOrder(OrderRequests orderRequests)
        {
            if (orderRequests.orderRequestStatus.Equals(OrderRequestStatus.ACCEPTED))
            {
                Order order = _mapper.Map<Order>(orderRequests);
                order.pickupLocation=orderRequests.restaurant.restaurantLocation;
                order.dropoffLocation=orderRequests.DropLocation;
                List<CartItem> cartItems = orderRequests.cart.cartItems;
                List<OrderItem> orderItems = _mapper.Map<List<OrderItem>>(cartItems);
                foreach (OrderItem item in orderItems)
                {
                    item.id = 0;
                    item.order = order;
                }
                order.orderItems = orderItems;
                order.orderStatus =OrderStatus.ACCEPTED;
                var updateOrder = _context.Order.Add(order).Entity;
                await _context.SaveChangesAsync();
                return updateOrder;
            }
            throw new Exception("Can not create order as OrderRequest status is not Accepted");
        }

        public async Task<Order> getOrderById(long OrderId)
        {
        return await _context.Order.FindAsync(OrderId) ?? throw new ResourceNotFoundException("Order Not Found with Id" + OrderId);
        }

        public async Task<Order> GetOrderByIdAsync(long orderId)
        {
            var order = await _context.Order.FindAsync(orderId);
            return order ?? throw new ResourceNotFoundException($"Order not found with ID {orderId}");
        }


        public async Task<Order> saveOrder(Order order)
        {
            var orderCreated = _context.Order.Add(order).Entity;
            await _context.SaveChangesAsync();
            return orderCreated;
        }

        public async Task<Order> updateOrderStatus(long OrderId, OrderStatus orderStatus)
        {
            Order order = await getOrderById(OrderId);
            order.orderStatus = orderStatus;
           var updateOrder = _context.Update(order).Entity;
            await _context.SaveChangesAsync();
            return updateOrder;
        }

    }
}
