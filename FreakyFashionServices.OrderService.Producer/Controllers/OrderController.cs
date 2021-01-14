using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FreakyFashionServices.OrderService.Producer.Models;
using FreakyFashionServices.Shared;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FreakyFashionServices.OrderService.Producer.Controllers
{
    [ApiController]
    [Route("/api/order/")]
    public class OrderController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IHttpClientFactory _clientFactory;

        public OrderController(IPublishEndpoint publishEndpoint,IHttpClientFactory clientFactory)
        {
            _publishEndpoint = publishEndpoint;
            _clientFactory = clientFactory;
        }


        [HttpPost]
        public async Task<IActionResult> Post(OrderDto order)
        {

            var basket = await GetBasket(order.CustomIdentifier);

            if (basket != null)
            {
                RabbitOrderMessage rabbitOrderMessage = new RabbitOrderMessage();


                rabbitOrderMessage.CustomIdentifier = order.CustomIdentifier;
                rabbitOrderMessage.FirstName = order.FirstName;
                rabbitOrderMessage.LastName = order.LastName;
                foreach (var item in basket.Items)
                {
                    FreakyFashionServices.Shared.Item tempItem = new FreakyFashionServices.Shared.Item(item.Id,item.Name,item.Description,item.Price,item.AvailableStock); 
                    rabbitOrderMessage.Items.Add(tempItem);
                    
                }
                
                
                await _publishEndpoint.Publish<RabbitOrderMessage>(rabbitOrderMessage);
                return Accepted();
            }

            return NotFound("Basket Not found");

        }

        private async Task<Basket> GetBasket(string basketIdentifier)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://basket:80/api/basket/" + basketIdentifier);
            

            request.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var serializedBasket = await response.Content.ReadAsStringAsync();

                var serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var basket = JsonSerializer.Deserialize<Basket>(serializedBasket, serializeOptions);
                return basket;
                
            }

            return null;

        }
        
        
    }
    
    
   


    public class OrderDto
    {
        public string CustomIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
    }
    
    
    
}