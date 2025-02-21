using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Index.HPRtree;
using Zomato.Data;
using Zomato.Entity.Enum;
using Zomato.Exceptions.CustomExceptionHandler;
using Zomato.Model;

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

        public Order cancelOrder(long OrderId)
        {
            Order order = getOrderById(OrderId);
            if (order.orderStatus.Equals(OrderStatus.ACCEPTED))
            {
                order.orderStatus = OrderStatus.CANCELLED;
               return _context.Order.Update(order).Entity;

            }
            throw new Exception("Can not cancel order as OrderRequest status is not Accepted earlier");

        }

        public Order createOrder(OrderRequests orderRequests)
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
                _context.Order.Add(order);
                _context.SaveChanges();
                return order;
            }
            throw new Exception("Can not create order as OrderRequest status is not Accepted");
        }

        public Order getOrderById(long OrderId)
        {
        return _context.Order.Find(OrderId) ?? throw new ResourceNotFoundException("Order Not Found with Id" + OrderId);
        }

        public Order saveOrder(Order order)
        {
            _context.Order.Add(order);
            _context.SaveChanges();
            return order;
        }

        public Order updateOrderStatus(long OrderId, OrderStatus orderStatus)
        {
                Order order = getOrderById(OrderId);
                order.orderStatus = orderStatus;
                return _context.Update(order).Entity;
        }

    }
}
