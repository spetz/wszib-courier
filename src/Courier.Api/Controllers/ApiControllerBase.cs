using System;
using System.Threading.Tasks;
using Courier.Api.Framework;
using Courier.Core.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Courier.Api.Controllers
{
    [Route("[controller]")]
    public abstract class ApiControllerBase : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;
        protected Guid UserId => User.Identity.IsAuthenticated ?
            Guid.Parse(User.Identity.Name) : Guid.Empty;

        protected ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        protected async Task DispatchAsync<T>(T command) where T : ICommand
        {
            command.UserId = UserId;
            await _commandDispatcher.DispatchAsync(command);
        }

        protected async Task<TResult> DispatchAsync<TCommand,TResult>(TCommand command) where TCommand : ICommand
        {
            command.UserId = UserId;
            
            return await _commandDispatcher.DispatchAsync<TCommand,TResult>(command);
        }
    }
}