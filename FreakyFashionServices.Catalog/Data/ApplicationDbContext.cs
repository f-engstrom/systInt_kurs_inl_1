using FreakyFashionServices.Catalog.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FreakyFashionServices.Catalog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        internal DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().HasData(new Item(1, "Black T-Shirt", "lorem ipsum", 299, 12));
        }
    }
}