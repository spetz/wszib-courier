using System;
using System.Threading.Tasks;
using Courier.Api.Framework;
using Courier.Core.Commands.Parcels;
using Courier.Core.Queries;
using Courier.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Courier.Api.Controllers
{
    public class ParcelsController : ApiControllerBase
    {
        private readonly IParcelService _parcelService;

        public ParcelsController(ICommandDispatcher commandDispatcher,
            IParcelService parcelService) : base(commandDispatcher)
        {
            _parcelService = parcelService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var parcel = await _parcelService.GetAsync(id);
            if (parcel == null)
            {
                return NotFound();
            }

            return Ok(parcel);
        }

        [HttpGet("")]
        public async Task<IActionResult> Get([FromQuery] BrowseParcels query)
            => Ok(await _parcelService.BrowseAsync(query));

        [HttpGet("delivery-available/{address}")]
        public async Task<IActionResult> DeliveryAvailable(string address)
        {
            var deliveryAvailable = await _parcelService.DeliveryAvailableAsync(address);
            if (deliveryAvailable)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody] CreateParcel command)
        {
            await DispatchAsync(command);

            return CreatedAtAction(nameof(Get), new { id = command.Id}, null);
        }
    }
}