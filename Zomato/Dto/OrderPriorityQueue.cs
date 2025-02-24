using Zomato.Entity.Enum;

namespace Zomato.Dto
{  
        public class OrderPriorityQueue : IComparable<OrderPriorityQueue>
        {
            public long Id { get; set; }
            public long CreationTime { get; set; }
            public int RestaurantPriority { get; set; }
            public PaymentMethod PaymentMethod { get; set; }

            public OrderPriorityQueue(long orderId, PaymentMethod paymentMethod, int restaurantPriority, long creationTime)
            {
                Id = orderId;
                CreationTime = creationTime;
                RestaurantPriority = restaurantPriority;
                PaymentMethod = paymentMethod;
            }

            public int CompareTo(OrderPriorityQueue other)
            {
                if (other == null) return 1;

                // Prioritize older orders (>5 minutes)
                if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - CreationTime > TimeSpan.FromMinutes(5).TotalMilliseconds)
                {
                    return -1; // Older orders get priority
                }

                // Compare restaurant priority (higher value = higher priority)
                int restaurantPriorityComparison = other.RestaurantPriority.CompareTo(RestaurantPriority);
                if (restaurantPriorityComparison != 0)
                {
                    return restaurantPriorityComparison;
                }

                // Compare payment method priority (UPI > Wallet > Cash)
                int thisPaymentPriority = GetPaymentPriority(PaymentMethod);
                int otherPaymentPriority = GetPaymentPriority(other.PaymentMethod);
                int paymentComparison = otherPaymentPriority.CompareTo(thisPaymentPriority);
                if (paymentComparison != 0)
                {
                    return paymentComparison;
                }

                // As a last resort, use FIFO (older orders first)
                return CreationTime.CompareTo(other.CreationTime);
            }

            private static readonly Dictionary<PaymentMethod, int> PaymentPriorityMap = new()
        {
            { PaymentMethod.UPI, 3 },
            { PaymentMethod.WALLET, 2 },
            { PaymentMethod.CASH, 1 }
        };

            private int GetPaymentPriority(PaymentMethod paymentMethod)
            {
                return PaymentPriorityMap.TryGetValue(paymentMethod, out int priority) ? priority : 0;
            }
        }
}



