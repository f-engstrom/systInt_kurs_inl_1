using System.Collections.Generic;

namespace FreakyFashionServices.OrderService.Consumer.Data.Models
{
    internal class Order
    {
        public int Id { get; set; }
        public string CustomIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
}