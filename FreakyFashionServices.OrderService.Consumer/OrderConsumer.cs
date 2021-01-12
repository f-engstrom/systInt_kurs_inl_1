using System;
using System.Linq;
using System.Threading.Tasks;
using FreakyFashionServices.OrderService.Consumer.Data;
using FreakyFashionServices.OrderService.Consumer.Data.Models;
using FreakyFashionServices.Shared;
using MassTransit;

namespace FreakyFashionServices.OrderService.Consumer
{
    internal class OrderConsumer: IConsumer<RabbitOrderMessage>
    {
        private ApplicationDbContext _dbcontext;

        public OrderConsumer(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        
        public async Task Consume(ConsumeContext<RabbitOrderMessage> context)
        {

            await Console.Out.WriteAsync(context.Message.FirstName);

            Order order = new Order();

            order.CustomIdentifier = context.Message.CustomIdentifier;
            order.FirstName = context.Message.FirstName;
            order.LastName = context.Message.LastName;
            foreach (var item in context.Message.Items)
            {
                Item tempItem = new Item(item.Name,item.Description,item.Price,item.AvailableStock);
                order.Items.Add(tempItem);
            }
            
            _dbcontext.Add(order);
            _dbcontext.SaveChanges();


        }
    }
}