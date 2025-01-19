using Microsoft.Extensions.DependencyInjection;
using Shipping.Application.AutoMapper;
using Shipping.Application.Contracts;
using Shipping.Application.DTOs;
using Shipping.Application.Services;

namespace Shipping.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddKeyedScoped<ICommonService<VehicleDto, VehicleInsertDto, VehicleUpdateDto>, VehicleService>("VehicleService");
            services.AddKeyedScoped<ICommonService<DriverDto, DriverInsertDto, DriverUpdateDto>, DriverService>("DriverService");
            services.AddKeyedScoped<ICommonService<OrderDto, OrderInsertDto, OrderUpdateDto>, OrderService>("OrderService");
            services.AddKeyedScoped<ICommonService<ShipmentDto, ShipmentInsertDto, ShipmentUpdateDto>, ShipmentService>("ShipmentService");
            services.AddKeyedScoped<ICommonService<ShipmentTypeDto, ShipmentTypeInsertDto, ShipmentTypeUpdateDto>, ShipmentTypeService>("ShipmentTypeService");
            services.AddScoped<TrackingService>();
            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
