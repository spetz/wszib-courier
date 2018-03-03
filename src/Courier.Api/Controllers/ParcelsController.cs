using System;
using System.Threading.Tasks;
using Courier.Core.Commands.Parcels;
using Microsoft.AspNetCore.Mvc;

namespace Courier.Api.Controllers
{
    public class ParcelsController : ApiControllerBase
    {
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

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateParcel command)
        {
            return Ok();
        }       
    }
}