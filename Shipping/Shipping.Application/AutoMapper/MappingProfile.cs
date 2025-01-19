using AutoMapper;
using NetTopologySuite.Geometries;
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

            CreateMap<ShipmentTypeDto, ShipmentType>().ReverseMap();
            CreateMap<ShipmentTypeInsertDto, ShipmentType>().ReverseMap();
            CreateMap<ShipmentTypeUpdateDto, ShipmentType>().ReverseMap();

            CreateMap<ShipmentDto, Shipment>()
                .ForMember(ent => ent.OriginLocation, opt => opt.MapFrom(dto => new Point(dto.OriginLocationLongitude, dto.OriginLocationLatitude) { SRID = 4326 }))
                .ForMember(ent => ent.DestinationLocation, opt => opt.MapFrom(dto => new Point(dto.DestinationLocationLongitude, dto.DestinationLocationLatitude) { SRID = 4326 }))
                .ReverseMap()
                .ForMember(dto => dto.OriginLocationLatitude, opt => opt.MapFrom(ent => ent.OriginLocation.Y))
                .ForMember(dto => dto.OriginLocationLongitude, opt => opt.MapFrom(ent => ent.OriginLocation.X))
                .ForMember(dto => dto.DestinationLocationLatitude, opt => opt.MapFrom(ent => ent.DestinationLocation.Y))
                .ForMember(dto => dto.DestinationLocationLongitude, opt => opt.MapFrom(ent => ent.DestinationLocation.X));

            CreateMap<ShipmentInsertDto, Shipment>()
                .ForMember(ent => ent.OriginLocation, opt => opt.MapFrom(dto => new Point(dto.OriginLocationLongitude, dto.OriginLocationLatitude) { SRID = 4326 }))
                .ForMember(ent => ent.DestinationLocation, opt => opt.MapFrom(dto => new Point(dto.DestinationLocationLongitude, dto.DestinationLocationLatitude) { SRID = 4326 }))
                .ReverseMap()
                .ForMember(dto => dto.OriginLocationLatitude, opt => opt.MapFrom(ent => ent.OriginLocation.Y))
                .ForMember(dto => dto.OriginLocationLongitude, opt => opt.MapFrom(ent => ent.OriginLocation.X))
                .ForMember(dto => dto.DestinationLocationLatitude, opt => opt.MapFrom(ent => ent.DestinationLocation.Y))
                .ForMember(dto => dto.DestinationLocationLongitude, opt => opt.MapFrom(ent => ent.DestinationLocation.X));

            CreateMap<ShipmentUpdateDto, Shipment>()
                .ForMember(ent => ent.OriginLocation, opt => opt.MapFrom(dto => new Point(dto.OriginLocationLongitude, dto.OriginLocationLatitude) { SRID = 4326 }))
                .ForMember(ent => ent.DestinationLocation, opt => opt.MapFrom(dto => new Point(dto.DestinationLocationLongitude, dto.DestinationLocationLatitude) { SRID = 4326 }))
                .ReverseMap()
                .ForMember(dto => dto.OriginLocationLatitude, opt => opt.MapFrom(ent => ent.OriginLocation.Y))
                .ForMember(dto => dto.OriginLocationLongitude, opt => opt.MapFrom(ent => ent.OriginLocation.X))
                .ForMember(dto => dto.DestinationLocationLatitude, opt => opt.MapFrom(ent => ent.DestinationLocation.Y))
                .ForMember(dto => dto.DestinationLocationLongitude, opt => opt.MapFrom(ent => ent.DestinationLocation.X));

        }
    }
}
