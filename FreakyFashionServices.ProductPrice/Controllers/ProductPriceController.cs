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
                if (!string.IsNullOrEmpty(articleNr))
                {
                    ProductDto product = new ProductDto();

                    Console.WriteLine($"article nr ${articleNr}");


                    product.Price = rng.Next(100, 2000);
                    product.ArticleNumber = Convert.ToInt32(articleNr.Trim().Normalize());

                    productDtos.Add(product);
                }
            }

            return productDtos;
        }
    }

    public class ProductDto
    {
        public int ArticleNumber { get; set; }
        public int Price { get; set; }
    }
}