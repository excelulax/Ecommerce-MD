using Microsoft.AspNetCore.Mvc;
using Shipping.Application.DTOs;
using Shipping.Application.DTOs.Tracking;
using Shipping.Application.Services;
using Shipping.Infrastructure.Gateway;

namespace Shipping.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {
        private readonly TrackingService _service;
        private readonly IdentityService _identityService;

        public TrackingController(TrackingService service, IdentityService identityService)
        {
            _service = service;
            _identityService = identityService;
        }

        [HttpPost("CheckLocation")]
        public ActionResult<LocationVerificationResponseDTO> CheckLocation(DistanceVerificationDTO dto)
        {
            var response = _service.VerifyLocation(dto);
            return Ok(response);
        }

        [HttpPost("/login")]
        public async Task<ActionResult> Test(Request request)
        {
            var resul = await _identityService.PostRequestAsync(request);
            return Ok(resul);
        }
    }
}
