using Zomato.Entity;

namespace Zomato.Strategies
{
    public interface IPaymentStrategy
    {
        void ProcessPayment(Payment payment);
    }
}
