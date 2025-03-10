using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Zomato.Data;
using Zomato.Entity;
using Zomato.Entity.Enum;
using Zomato.Exceptions.CustomExceptionHandler;
using Zomato.Strategies.Zomato.Strategies;

namespace Zomato.Service.Impl
{
    public class PaymentService : IPaymentService
    {
        private readonly PaymentStrategyManager paymentStrategyManager;
        private readonly AppDbContext _context;

        public PaymentService(PaymentStrategyManager paymentStrategyManager, AppDbContext context)
        {
            this.paymentStrategyManager = paymentStrategyManager;
            _context = context;
        }

        public async Task<Payment> CreateNewPayment(Order order)
        {
            Payment payment = new Payment()
            {
                order = order,
                paymentMethod = order.paymentMethod,
                amount = order.totalPrice,
                paymentStatus = PaymentStatus.PENDING
            };
            if (order.paymentStatus.Equals(PaymentStatus.CONFIRMED))
            {
                payment.paymentStatus = PaymentStatus.CONFIRMED;
            }
            var created_Payment =_context.Payment.Add(payment).Entity;
            await _context.SaveChangesAsync();
            return created_Payment;
        }

        public async Task processPayment(Order order)
        {
            Payment payment = await _context.Payment.Where(u=>u.order ==order).FirstOrDefaultAsync()??  throw new ResourceNotFoundException("Payment not found for the Order with Id " + order.id);
            await paymentStrategyManager.GetPaymentStrategy(payment.paymentMethod).ProcessPayment(payment);
        }

        public async Task updatePaymentStatus(Payment payment, PaymentStatus status)
        {
            payment.paymentStatus = status;
            _context.Payment.Update(payment);
           await _context.SaveChangesAsync();
        }
    }
}
