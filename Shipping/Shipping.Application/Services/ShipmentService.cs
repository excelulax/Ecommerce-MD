using AutoMapper;
using Shipping.Application.Contracts;
using Shipping.Application.DTOs;
using Shipping.Domain.Contracts;
using Shipping.Domain.Entities;

namespace Shipping.Application.Services
{
    public class ShipmentService : ICommonService<ShipmentDto, ShipmentInsertDto, ShipmentUpdateDto>
    {
        private readonly ICommonRepository<Shipment> _repository;
        private readonly IMapper _mapper;

        public ShipmentService(ICommonRepository<Shipment> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ShipmentDto>> Get()
        {
            var shipments = await _repository.Get();
            var shipmentsDto = _mapper.Map<List<ShipmentDto>>(shipments);
            return shipmentsDto;
        }

        public async Task<ShipmentDto> GetById(Guid id)
        {
            var shipment = await _repository.GetById(id);
            var shipmentDto = _mapper.Map<ShipmentDto>(shipment);
            return shipmentDto;
        }
        public async Task<ShipmentDto> Add(ShipmentInsertDto insertDto)
        {
            var shipment = _mapper.Map<Shipment>(insertDto);
            await _repository.Add(shipment);
            await _repository.Save();
            var shipmentResult = _mapper.Map<ShipmentDto>(shipment);
            return shipmentResult;
        }
        public async Task<ShipmentDto> Update(ShipmentUpdateDto updateDto)
        {
            var shipment = _mapper.Map<Shipment>(updateDto);
            _repository.Update(shipment);
            await _repository.Save();
            var shipmentDto = _mapper.Map<ShipmentDto>(shipment);
            return shipmentDto;
        }

        public async Task<ShipmentDto> Delete(Guid id)
        {
            var shipment = await _repository.GetById(id);
            if (shipment is not null)
            {
                _repository.Delete(shipment);
                await _repository.Save();
                return _mapper.Map<ShipmentDto>(shipment);
            }
            return null;
        }

        //public bool IsLocationWithinRange(DistanceVerificationDTO distanceVerificationDTO)
        //{
        //    var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

        //    double centerLongitude = -64.734827;
        //    double centerLatitude = -21.531269;

        //    var centerPoint = geometryFactory.CreatePoint(new Coordinate(centerLongitude, centerLatitude));

        //    var originPoint = geometryFactory
        //        .CreatePoint(new Coordinate(distanceVerificationDTO.OriginLongitude, distanceVerificationDTO.OriginLatitude));

        //    var destinationPoint = geometryFactory
        //        .CreatePoint(new Coordinate(distanceVerificationDTO.DestinationLongitude, distanceVerificationDTO.DestinationLatitude));

        //    var maxDistance = 1000;

        //    var originDistance = centerPoint.IsWithinDistance(originPoint, maxDistance);
        //    var destinationDistance = centerPoint.IsWithinDistance(destinationPoint, maxDistance);

        //    return (originDistance && destinationDistance);
        //}
    }
}
