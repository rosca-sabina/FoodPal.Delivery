using FoodPal.Delivery.Data.Configurations;
using FoodPal.Delivery.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodPal.Delivery.Data
{
    public class DeliveryDbContext: DbContext
    {
        public DeliveryDbContext(DbContextOptions options): base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Models.Delivery> Deliveries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliveryEntityTypeConfiguration).Assembly);
        }
    }
}
