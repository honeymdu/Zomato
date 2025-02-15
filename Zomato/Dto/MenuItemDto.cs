using System.Runtime.InteropServices;
using Zomato.Entity.Enum;

namespace Zomato.Dto
{
    public class MenuItemDto
    {
        private long id { get; set; }
        private String imageUrl { get; set; }
        private String name { get; set; }
        private String dishDescription { get; set; }
        private Double price { get; set; }
        private FoodCategory foodCategory { get; set; }
        private List<String> ingredients { get; set; }
        private Boolean isAvailable { get; set; }
    }
}
