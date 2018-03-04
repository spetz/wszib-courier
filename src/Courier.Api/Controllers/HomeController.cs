using Courier.Api.Framework;
using Microsoft.AspNetCore.Mvc;

namespace Courier.Api.Controllers
{
    [Route("")]
    public class HomeController : ApiControllerBase
    {
        public HomeController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        public IActionResult Get()
            => Content("Welcome to Courier API.");
    }
}