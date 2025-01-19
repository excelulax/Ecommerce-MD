using Microsoft.AspNetCore.Mvc;
using Shipping.Application.Contracts;
using Shipping.Application.DTOs;

namespace Shipping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentTypeController : ControllerBase
    {
        private readonly ICommonService<ShipmentTypeDto, ShipmentTypeInsertDto, ShipmentTypeUpdateDto> _service;

        public ShipmentTypeController([FromKeyedServices("ShipmentTypeService")] ICommonService<ShipmentTypeDto, ShipmentTypeInsertDto, ShipmentTypeUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet("/shipmentType")]
        public async Task<ActionResult<IEnumerable<ShipmentTypeDto>>> GetAll()
        {
            var result = await _service.Get();
            return Ok(result);
        }

        [HttpGet("/shipmentType/{id}")]
        public async Task<ActionResult<ShipmentTypeDto>> Get(Guid id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpPost("/shipmentType")]
        public async Task<ActionResult<ShipmentTypeDto>> Post(ShipmentTypeInsertDto shipmentTypeInsertDto)
        {
            var result = await _service.Add(shipmentTypeInsertDto);
            return Ok(result);
        }

        [HttpPut("/shipmentType")]
        public async Task<ActionResult<ShipmentTypeDto>> Put(ShipmentTypeUpdateDto shipmentTypeUpdateDto)
        {
            var result = await _service.Update(shipmentTypeUpdateDto);
            return Ok(result);
        }

        [HttpDelete("/shipmentType/{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
