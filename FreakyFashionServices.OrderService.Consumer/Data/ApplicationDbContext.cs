using Microsoft.EntityFrameworkCore;
using Order = FreakyFashionServices.OrderService.Consumer.Data.Models.Order;

namespace FreakyFashionServices.OrderService.Consumer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        internal DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}