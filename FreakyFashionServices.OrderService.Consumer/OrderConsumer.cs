using System;
using System.Threading.Tasks;
using FreakyFashionServices.Shared;
using MassTransit;

namespace FreakyFashionServices.OrderService.Consumer
{
    public class OrderConsumer: IConsumer<Order>
    {
        public async Task Consume(ConsumeContext<Order> context)
        {

            await Console.Out.WriteAsync(context.Message.FirstName);
        }
    }
}