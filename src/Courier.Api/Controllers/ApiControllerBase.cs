using Microsoft.AspNetCore.Mvc;

namespace Courier.Api.Controllers
{
    [Route("[controller]")]
    public abstract class ApiControllerBase : Controller
    {
    }
}