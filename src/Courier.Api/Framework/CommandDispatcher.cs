using System;
using System.Threading.Tasks;
using Courier.Core.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Courier.Api.Framework
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly ServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceCollection serviceCollection)
        {
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public async Task DispatchAsync<T>(T command) where T : ICommand
        {
            var handler = _serviceProvider.GetService<ICommandHandler<T>>();
            if (handler == null)
            {
                throw new ArgumentException($"Command handler: '{typeof(T).Name}' was not found.",
                    nameof(handler));
            }
            await handler.HandleAsync(command);
        }
    }
}