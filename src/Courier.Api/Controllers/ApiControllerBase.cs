using System;
using Microsoft.AspNetCore.Mvc;

namespace Courier.Api.Controllers
{
    [Route("[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        protected Guid UserId => User.Identity.IsAuthenticated ?
            Guid.Parse(User.Identity.Name) : Guid.Empty;
    }
}