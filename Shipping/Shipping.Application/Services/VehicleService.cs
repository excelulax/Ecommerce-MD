using AutoMapper;
using Shipping.Application.Contracts;
using Shipping.Application.DTOs;
using Shipping.Domain.Contracts;
using Shipping.Domain.Entities;

namespace Shipping.Application.Services
{
    public class VehicleService : ICommonService<VehicleDto, VehicleInsertDto, VehicleUpdateDto>
    {
        private readonly ICommonRepository<Vehicle> _repository;
        private readonly IMapper _mapper;

        public VehicleService(ICommonRepository<Vehicle> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<VehicleDto>> Get()
        {
            var vehicles = await _repository.Get();
            var vehiclesDto = _mapper.Map<List<VehicleDto>>(vehicles);
            return vehiclesDto;
        }
        public async Task<VehicleDto> GetById(Guid id)
        {
            var vehicle = await _repository.GetById(id);
            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);
            return vehicleDto;
        }

        public async Task<VehicleDto> Add(VehicleInsertDto insertDto)
        {
            var vehicle = _mapper.Map<Vehicle>(insertDto);
            await _repository.Add(vehicle);
            await _repository.Save();
            var vehicleResult = _mapper.Map<VehicleDto>(vehicle);
            return vehicleResult;
        }
        public async Task<VehicleDto> Update(VehicleUpdateDto updateDto)
        {
            var vehicle = _mapper.Map<Vehicle>(updateDto);
            _repository.Update(vehicle);
            await _repository.Save();
            var vehicleDto = _mapper.Map<VehicleDto>(vehicle);
            return vehicleDto;
        }
        public async Task<VehicleDto> Delete(Guid id)
        {
            var vehicle = await _repository.GetById(id);
            if (vehicle is not null)
            {
                _repository.Delete(vehicle);
                await _repository.Save();
                return _mapper.Map<VehicleDto>(vehicle);
            }
            return null;
        }
    }
}
