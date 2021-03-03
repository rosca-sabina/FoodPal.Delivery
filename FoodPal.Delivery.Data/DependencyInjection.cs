using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodPal.Delivery.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<DeliveryDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DeliveryConnectionString")));
        }
    }
}
