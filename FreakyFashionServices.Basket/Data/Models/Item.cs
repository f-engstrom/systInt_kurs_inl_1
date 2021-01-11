namespace FreakyFashionServices.Basket.Data.Models
{
    public class Item
    {
        public Item(long id, string name, string description, long price, long availableStock)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            AvailableStock = availableStock;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public long Price { get; set; }

        public long AvailableStock { get; set; }
    }
    
     
}