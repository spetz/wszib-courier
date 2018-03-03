using Microsoft.AspNetCore.Mvc;

namespace Courier.Api.Controllers
{
    [Route("")]
    public class HomeController : ApiControllerBase
    {
        public IActionResult Get()
            => Content("Welcome to Courier API.");
    }
}