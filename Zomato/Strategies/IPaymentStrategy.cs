using Zomato.Entity;

namespace Zomato.Strategies
{
    public interface IPaymentStrategy
    {
        Task ProcessPayment(Payment payment);
    }
}
