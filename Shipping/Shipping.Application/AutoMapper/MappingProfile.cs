using AutoMapper;
using Shipping.Application.DTOs;
using Shipping.Domain.Entities;

namespace Shipping.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleDto, Vehicle>().ReverseMap();
            CreateMap<VehicleInsertDto, Vehicle>().ReverseMap();
            CreateMap<VehicleUpdateDto, Vehicle>().ReverseMap();

            CreateMap<DriverDto, Driver>().ReverseMap();
            CreateMap<DriverInsertDto, Driver>().ReverseMap();
            CreateMap<DriverUpdateDto, Driver>().ReverseMap();

            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<OrderInsertDto, Order>().ReverseMap();
            CreateMap<OrderUpdateDto, Order>().ReverseMap();
        }
    }
}
