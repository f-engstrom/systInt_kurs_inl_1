using System.Collections.Generic;

namespace FreakyFashionServices.Basket.Data.Models
{
    public class Basket
    {
        public List<Item> Items { get; set; } = new List<Item>();
    }
}