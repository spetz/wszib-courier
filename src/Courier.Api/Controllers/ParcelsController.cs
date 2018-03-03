using System;
using System.Threading.Tasks;
using Courier.Core.Commands.Parcels;
using Courier.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Courier.Api.Controllers
{
    public class ParcelsController : ApiControllerBase
    {
        private readonly ILocationService _locationService;

        public ParcelsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok();
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpGet("delivery-available/{address}")]
        public async Task<IActionResult> DeliveryAvailable(string address)
        {
            var dto = await _locationService.GetAsync(address);
            if (dto != null)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateParcel command)
        {
            return Ok();
        }
    }
}