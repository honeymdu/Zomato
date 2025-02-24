using Zomato.Entity.Enum;
using Zomato.Entity;

namespace Zomato.Service
{

    public interface IPaymentService
    {
        public void processPayment(Order order);

        public Payment CreateNewPayment(Order order);

        public void updatePaymentStatus(Payment payment, PaymentStatus status);

    }
}
