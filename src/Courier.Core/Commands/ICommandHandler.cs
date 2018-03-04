using System.Threading.Tasks;

namespace Courier.Core.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }

    public interface ICommandHandler<TCommand,TResult> where TCommand : ICommand
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}