using Microsoft.AspNetCore.Mvc;
using Shipping.Application.DTOs;
using Shipping.Application.DTOs.Tracking;
using Shipping.Application.Services;

namespace Shipping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly TrackingService _service;

        public TrackingController(TrackingService service)
        {
            _service = service;
        }

        [HttpPost("CheckLocation")]
        public ActionResult<LocationVerificationResponseDTO> CheckLocation(DistanceVerificationDTO dto)
        {
            var response = _service.VerifyLocation(dto);
            return Ok(response);
        }
    }
}
