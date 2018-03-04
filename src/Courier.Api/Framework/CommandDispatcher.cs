using System;
using System.Threading.Tasks;
using Autofac;
using Courier.Core.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Courier.Api.Framework
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            var handler = _context.Resolve<ICommandHandler<T>>();
            if (handler == null)
            {
                throw new ArgumentException($"Command handler: '{typeof(T).Name}' was not found.",
                    nameof(handler));
            }
            await handler.HandleAsync(command);
        }

        public async Task<TResult> DispatchAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand
        {
            var handler = _context.Resolve<ICommandHandler<TCommand,TResult>>();
            if (handler == null)
            {
                throw new ArgumentException($"Command handler: '{typeof(TCommand).Name}' was not found.",
                    nameof(handler));
            }

            return await handler.HandleAsync(command);
        }
    }
}