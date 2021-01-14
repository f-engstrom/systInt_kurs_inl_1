using System.Collections.Generic;

namespace FreakyFashionServices.OrderService.Producer.Models
{
    public class Basket
    {
        public List<Item> Items { get; set; } = new List<Item>();
    }
}