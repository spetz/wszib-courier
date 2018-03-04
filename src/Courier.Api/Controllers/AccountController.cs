using System.Threading.Tasks;
using Courier.Api.Framework;
using Courier.Core.Commands.Users;
using Microsoft.AspNetCore.Mvc;

namespace Courier.Api.Controllers
{
    [Route("")]
    public class AccountController : ApiControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public AccountController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUp command)
        {
            await _commandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignIn command)
        {
            await _commandDispatcher.DispatchAsync(command);

            return Ok();
        }
    }
}