using Microsoft.AspNetCore.Mvc;
using Shipping.Application.Contracts;
using Shipping.Application.DTOs;

namespace Shipping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly ICommonService<VehicleDto, VehicleInsertDto, VehicleUpdateDto> _service;

        public VehicleController([FromKeyedServices("VehicleService")] ICommonService<VehicleDto, VehicleInsertDto, VehicleUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet("/vehicle")]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetAll()
        {
            var result = await _service.Get();
            return Ok(result);
        }

        [HttpGet("/vehicle/{id}")]
        public async Task<ActionResult<VehicleDto>> Get(Guid id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpPost("/vehicle")]
        public async Task<ActionResult<VehicleDto>> Post(VehicleInsertDto vehicleInsertDto)
        {
            var result = await _service.Add(vehicleInsertDto);
            return Ok(result);
        }

        [HttpPut("/vehicle")]
        public async Task<ActionResult<VehicleDto>> Put(VehicleUpdateDto vehicleUpdateDto)
        {
            var result = await _service.Update(vehicleUpdateDto);
            return Ok(result);
        }

        [HttpDelete("/vehicle/{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
