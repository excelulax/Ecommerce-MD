using AutoMapper;
using Shipping.Application.Contracts;
using Shipping.Application.DTOs;
using Shipping.Domain.Contracts;
using Shipping.Domain.Entities;

namespace Shipping.Application.Services
{
    public class DriverService : ICommonService<DriverDto, DriverInsertDto, DriverUpdateDto>
    {
        private readonly ICommonRepository<Driver> _repository;
        private readonly IMapper _mapper;

        public DriverService(ICommonRepository<Driver> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DriverDto>> Get()
        {
            var drivers = await _repository.Get();
            var driversDto = _mapper.Map<List<DriverDto>>(drivers);
            return driversDto;
        }
        public async Task<DriverDto> GetById(Guid id)
        {
            var driver = await _repository.GetById(id);
            var driverDto = _mapper.Map<DriverDto>(driver);
            return driverDto;
        }
        public async Task<DriverDto> Add(DriverInsertDto insertDto)
        {
            var driver = _mapper.Map<Driver>(insertDto);
            await _repository.Add(driver);
            await _repository.Save();
            var driverResult = _mapper.Map<DriverDto>(driver);
            return driverResult;
        }
        public async Task<DriverDto> Update(DriverUpdateDto updateDto)
        {
            var driver = _mapper.Map<Driver>(updateDto);
            _repository.Update(driver);
            await _repository.Save();
            var driverDto = _mapper.Map<DriverDto>(driver);
            return driverDto;
        }
        public async Task<DriverDto> Delete(Guid id)
        {
            var driver = await _repository.GetById(id);
            if (driver is not null)
            {
                _repository.Delete(driver);
                await _repository.Save();
                return _mapper.Map<DriverDto>(driver);
            }
            return null;
        }
    }
}
