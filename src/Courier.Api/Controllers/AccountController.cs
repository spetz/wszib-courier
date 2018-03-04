using System.Threading.Tasks;
using Courier.Api.Framework;
using Courier.Core.Commands.Users;
using Courier.Core.Dto;
using Courier.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Courier.Api.Controllers
{
    [Route("")]
    public class AccountController : ApiControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IUserService _userService;

        public AccountController(ICommandDispatcher commandDispatcher,
            IUserService userService)
        {
            _commandDispatcher = commandDispatcher;
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<IActionResult> Get()
        {
            //var user = await _userService.GetAsync(...)

            return Ok("Hello ...");
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
            var jwt = await _commandDispatcher.DispatchAsync<SignIn,JsonWebTokenDto>(command);

            return Ok(jwt);
        }
    }
}