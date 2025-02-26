using Zomato.Entity.Enum;
using Zomato.Entity;

namespace Zomato.Service
{

    public interface IPaymentService
    {
        public Task processPayment(Order order);

        public Task<Payment> CreateNewPayment(Order order);

        public Task updatePaymentStatus(Payment payment, PaymentStatus status);

    }
}
