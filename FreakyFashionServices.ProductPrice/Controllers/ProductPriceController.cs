using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace FreakyFashionServices.ProductPrice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductPriceController : ControllerBase
    {
        [HttpGet]
        public List<ProductDto> Get(string products)
        {
            var rng = new Random();

            Console.WriteLine($"products ${products}");

            var splitProducts = products.Split(",");

            List<ProductDto> productDtos = new List<ProductDto>();

            foreach (var articleNr in splitProducts)
            {
                ProductDto product = new ProductDto();
                
                product.Price = rng.Next(100, 2000);
                product.ArticleNumber = articleNr;

                productDtos.Add(product);
            }

            return productDtos;
        }
    }

   public class ProductDto
    {
        public string ArticleNumber { get; set; }
        public int Price { get; set; }
    }
}