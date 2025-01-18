using Microsoft.AspNetCore.Mvc;
using Shipping.Application.Contracts;
using Shipping.Application.DTOs;

namespace Shipping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly ICommonService<DriverDto, DriverInsertDto, DriverUpdateDto> _service;

        public DriverController([FromKeyedServices("DriverService")] ICommonService<DriverDto, DriverInsertDto, DriverUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet("/driver")]
        public async Task<ActionResult<IEnumerable<DriverDto>>> GetAll()
        {
            var result = await _service.Get();
            return Ok(result);
        }

        [HttpGet("/driver/{id}")]
        public async Task<ActionResult<DriverDto>> Get(Guid id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpPost("/driver")]
        public async Task<ActionResult<DriverDto>> Post(DriverInsertDto driverInsertDto)
        {
            var result = await _service.Add(driverInsertDto);
            return Ok(result);
        }

        [HttpPut("/driver")]
        public async Task<ActionResult<DriverDto>> Put(DriverUpdateDto driverUpdateDto)
        {
            var result = await _service.Update(driverUpdateDto);
            return Ok(result);
        }

        [HttpDelete("/driver/{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
