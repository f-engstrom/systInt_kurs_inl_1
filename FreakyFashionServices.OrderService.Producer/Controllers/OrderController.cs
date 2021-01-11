using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreakyFashionServices.Shared;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public async Task<IActionResult> Post(Order order)
        {
            await _publishEndpoint.Publish<Order>(order);
            return Ok();
        }
    }
}