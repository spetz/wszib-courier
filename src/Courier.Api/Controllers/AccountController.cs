using System.Threading.Tasks;
using Courier.Api.Framework;
using Courier.Core.Commands.Users;
using Courier.Core.Dto;
using Courier.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Courier.Api.Controllers
{
    [Route("")]
    public class AccountController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(ICommandDispatcher commandDispatcher,
            IUserService userService) : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            //var user = await _userService.GetAsync(...)

            return Ok($"Hello user with id: '{UserId}'.");
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUp command)
        {
            await DispatchAsync(command);

            return Ok();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignIn command)
        {
            var jwt = await DispatchAsync<SignIn,JsonWebTokenDto>(command);

            return Ok(jwt);
        }
    }
}