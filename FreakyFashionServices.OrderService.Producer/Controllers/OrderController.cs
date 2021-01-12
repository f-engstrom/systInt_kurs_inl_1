using System.Threading.Tasks;
using FreakyFashionServices.Shared;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FreakyFashionServices.OrderService.Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }


        [HttpPost]
        public async Task<IActionResult> Post(RabbitOrderMessage order)
        {
            await _publishEndpoint.Publish<RabbitOrderMessage>(order);
            return Ok();
        }
    }
}