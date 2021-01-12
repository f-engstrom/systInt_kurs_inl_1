using System.Linq;
using System.Threading.Tasks;
using FreakyFashionServices.OrderService.Consumer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreakyFashionServices.OrderService.Consumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            return  Ok( _context.Orders.Include(x=> x.Items).ToList());
        }
    }
}