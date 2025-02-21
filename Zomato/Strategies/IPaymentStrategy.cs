using Zomato.Model;

namespace Zomato.Strategies
{
    public interface IPaymentStrategy
    {
        void ProcessPayment(Payment payment);
    }
}
