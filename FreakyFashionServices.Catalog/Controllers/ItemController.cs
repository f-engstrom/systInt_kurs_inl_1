using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreakyFashionServices.Catalog.Data;
using FreakyFashionServices.Catalog.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FreakyFashionServices.Catalog.Controllers
{
    [ApiController]
    [Route("/api/catalog/items")]
    public class ItemController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}", Name = "GetItemById")]
        public IActionResult GetCategory(int id)
        {

            Item category = _context.Items.FirstOrDefault(x => x.Id == id);

            if (category == null)
            {
                return new BadRequestResult();

            }


            return new JsonResult(category);


        }


        [HttpGet]
        public IActionResult Get()
        {

            var Items = _context.Items
                .ToList();

            if (Items == null)
            {
                return new BadRequestResult();

            }


            return new JsonResult(Items);


        }
    }
}
