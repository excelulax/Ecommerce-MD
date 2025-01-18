using Microsoft.AspNetCore.Mvc;
using Shipping.Application.Contracts;
using Shipping.Application.DTOs;

namespace Shipping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICommonService<OrderDto, OrderInsertDto, OrderUpdateDto> _service;

        public OrderController([FromKeyedServices("OrderService")] ICommonService<OrderDto, OrderInsertDto, OrderUpdateDto> service)
        {
            _service = service;
        }
        [HttpGet("/order")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var result = await _service.Get();
            return Ok(result);
        }

        [HttpGet("/order/{id}")]
        public async Task<ActionResult<OrderDto>> Get(Guid id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [HttpPost("/order")]
        public async Task<ActionResult<OrderDto>> Post(OrderInsertDto orderInsertDto)
        {
            var result = await _service.Add(orderInsertDto);
            return Ok(result);
        }

        [HttpPut("/order")]
        public async Task<ActionResult<OrderDto>> Put(OrderUpdateDto orderUpdateDto)
        {
            var result = await _service.Update(orderUpdateDto);
            return Ok(result);
        }

        [HttpDelete("/order/{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}
