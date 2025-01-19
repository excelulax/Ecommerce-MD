using AutoMapper;
using Shipping.Application.Contracts;
using Shipping.Application.DTOs;
using Shipping.Domain.Contracts;
using Shipping.Domain.Entities;

namespace Shipping.Application.Services
{
    public class ShipmentTypeService : ICommonService<ShipmentTypeDto, ShipmentTypeInsertDto, ShipmentTypeUpdateDto>
    {
        private readonly ICommonRepository<ShipmentType> _repository;
        private readonly IMapper _mapper;

        public ShipmentTypeService(ICommonRepository<ShipmentType> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShipmentTypeDto>> Get()
        {
            var shipmentType = await _repository.Get();
            var shipmentTypeDto = _mapper.Map<List<ShipmentTypeDto>>(shipmentType);
            return shipmentTypeDto;
        }

        public async Task<ShipmentTypeDto> GetById(Guid id)
        {
            var shipmentType = await _repository.GetById(id);
            var shipmentTypeDto = _mapper.Map<ShipmentTypeDto>(shipmentType);
            return shipmentTypeDto;
        }

        public async Task<ShipmentTypeDto> Add(ShipmentTypeInsertDto insertDto)
        {
            var shipmentType = _mapper.Map<ShipmentType>(insertDto);
            await _repository.Add(shipmentType);
            await _repository.Save();
            var shipmentTypeResult = _mapper.Map<ShipmentTypeDto>(shipmentType);
            return shipmentTypeResult;
        }

        public async Task<ShipmentTypeDto> Update(ShipmentTypeUpdateDto updateDto)
        {
            var shipmentType = _mapper.Map<ShipmentType>(updateDto);
            _repository.Update(shipmentType);
            await _repository.Save();
            var shipmentTypeDto = _mapper.Map<ShipmentTypeDto>(shipmentType);
            return shipmentTypeDto;
        }

        public async Task<ShipmentTypeDto> Delete(Guid id)
        {
            var shipmentType = await _repository.GetById(id);
            if (shipmentType is not null)
            {
                _repository.Delete(shipmentType);
                await _repository.Save();
                return _mapper.Map<ShipmentTypeDto>(shipmentType);
            }
            return null;
        }
    }
}
