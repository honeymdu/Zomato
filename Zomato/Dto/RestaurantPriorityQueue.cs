using Zomato.Entity;

namespace Zomato.Dto
{
        public class RestaurantPriorityQueue : IComparable<RestaurantPriorityQueue>
        {
            public Restaurant Restaurant { get; set; }
            public int Priority { get; set; }

            public RestaurantPriorityQueue(Restaurant restaurant, int priority)
            {
                Restaurant = restaurant;
                Priority = priority;
            }

            public int CompareTo(RestaurantPriorityQueue other)
            {
                if (other == null) return 1;
                return other.Priority.CompareTo(Priority); // Higher priority comes first
            }

            public override string ToString()
            {
                return $"RestaurantPriorityQueue{{ restaurant={(Restaurant != null ? Restaurant.name : "null")}, priority={Priority} }}";
            }
        }
}


