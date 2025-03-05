using System.Runtime.InteropServices;
using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class MenuItemDto
    {
        public long id { get; set; }
        public String imageUrl { get; set; }
        public String name { get; set; }
        public String dishDescription { get; set; }
        public Double price { get; set; }
        public FoodCategory foodCategory { get; set; }
        public List<String> ingredients { get; set; }
        public Boolean isAvailable { get; set; }
    }
}
