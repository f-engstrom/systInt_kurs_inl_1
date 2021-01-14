using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FreakyFashionServices.Gateway.Models;
using Microsoft.AspNetCore.Mvc;
using Ocelot.Responses;

namespace FreakyFashionServices.Gateway.Controllers
{
    [ApiController]
    [Route("/api/products")]
    public class ProductsAggregationController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductsAggregationController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        [HttpGet]
        public async Task<List<Item>> Get()
        {
            var products = await GetProducts();

            string articleNrs ="";
            foreach (var product in products)
            {
                articleNrs += product.Id +",";
            }
            

            var arcticleNrsWithPrices = await GetPrices(articleNrs);

            List<Item> productsWithFetchedPrices = new List<Item>();
            
            foreach (var arcticle in arcticleNrsWithPrices)
            {
                foreach (var product in products)
                {
                    if (product.Id == Convert.ToInt64(arcticle.ArticleNumber))
                    {
                        productsWithFetchedPrices.Add(new Item(product.Id,product.Name,product.Description,arcticle.Price,product.AvailableStock));
                    }
                }
                
            }
    
            return productsWithFetchedPrices;
        }


        private async Task<List<Item>> GetProducts()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://catalog:80/api/catalog/items");


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

                var items = JsonSerializer.Deserialize<List<Item>>(serializedBasket, serializeOptions);
                return items;
            }

            return null;
        }

        private async Task<List<ProductDto>> GetPrices(string articleNrs)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "http://productprice:80/productprice?products="+articleNrs);


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

                var arcticleNrsWithPrices = JsonSerializer.Deserialize<List<ProductDto>>(serializedBasket, serializeOptions);
                return arcticleNrsWithPrices;
            }

            return null;
        }
        
        
    }

    public class ProductDto
    {
        public int ArticleNumber { get; set; }
        public int Price { get; set; }
    }
}