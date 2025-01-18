using Microsoft.AspNetCore.Mvc;
using Shipping.Application.Contracts;
using Shipping.Application.DTOs;

namespace Shipping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly ICommonService<ShipmentDto, ShipmentInsertDto, ShipmentUpdateDto> _service;

        public ShipmentController([FromKeyedServices("ShipmentService")] ICommonService<ShipmentDto, ShipmentInsertDto, ShipmentUpdateDto> service)
        {
            _service = service;
        }

        [HttpGet("/shipment")]
        public async Task<ActionResult<IEnumerable<ShipmentDto>>> GetAll()
        {
            var result = await _service.Get();
            return Ok(result);
        }

        [HttpGet("/shipment/{id}")]
        public async Task<ActionResult<ShipmentDto>> Get(Guid id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpPost("/shipment")]
        public async Task<ActionResult<ShipmentDto>> Post(ShipmentInsertDto shipmentInsertDto)
        {
            var result = await _service.Add(shipmentInsertDto);
            return Ok(result);
        }

        [HttpPut("/shipment")]
        public async Task<ActionResult<ShipmentDto>> Put(ShipmentUpdateDto shipmentUpdateDto)
        {
            var result = await _service.Update(shipmentUpdateDto);
            return Ok(result);
        }

        [HttpDelete("/shipment/{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
