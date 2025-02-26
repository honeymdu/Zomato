using System.Runtime.InteropServices;
using Zomato.Entity.Enum;
using Zomato.Entity;
namespace Zomato.Service
{
    public interface IOrderService
    {

        Task<Order> updateOrderStatus(long OrderId, OrderStatus orderStatus);

        Task<Order> getOrderById(long OrderId);

        Task<Order> GetOrderByIdAsync(long orderId);

        Task<Order> createOrder(OrderRequests orderRequests);

        Task<Order> cancelOrder(long OrderId);

        Task<Order> saveOrder(Order order);

    }
}
