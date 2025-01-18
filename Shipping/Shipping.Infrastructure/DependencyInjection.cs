using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shipping.Domain.Contracts;
using Shipping.Domain.Entities;
using Shipping.Infrastructure.Context;
using Shipping.Infrastructure.Repositories;

namespace Shipping.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShippingDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("sqlServerDataBase"),
                    sqlServer => sqlServer.UseNetTopologySuite()
                    );
            });

            services.AddScoped<ICommonRepository<Vehicle>, VehicleRepository>();
            services.AddScoped<ICommonRepository<Driver>, DriverRepository>();
            services.AddScoped<ICommonRepository<Order>, OrderRepository>();
            return services;
        }
    }
}
