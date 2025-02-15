using System.Runtime.InteropServices;
using Zomato.Entity.Enum;
using Zomato.Model;

namespace Zomato.Service
{
    public interface IOrderService
    {

        Order updateOrderStatus(long OrderId, OrderStatus orderStatus);

        Order getOrderById(long OrderId);

        Order createOrder(OrderRequests orderRequests);

        Order cancelOrder(long OrderId);

        Order saveOrder(Order order);

    }
}
