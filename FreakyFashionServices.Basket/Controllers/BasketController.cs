using System.Threading.Tasks;
using FreakyFashionServices.Basket.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace FreakyFashionServices.Basket.Controllers
{
    [ApiController]
    [Route("/api/basket/")]
    public class BasketController : ControllerBase
    {
        private readonly IDistributedCache _cache;


        public BasketController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Data.Models.Basket basket)
        {
            await _cache.SetRecordAsync(id.ToString(), basket);


            return new NoContentResult();
        }


        [HttpGet("{id}")]
        public async Task<Data.Models.Basket> GetBasket(int id)
        {
            var basket = await _cache.GetRecordAsync<Data.Models.Basket>(id.ToString());
            
            
            return basket;
        }
    }
}