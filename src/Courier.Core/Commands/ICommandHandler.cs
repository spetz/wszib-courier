using System.Threading.Tasks;

namespace Courier.Core.Commands
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}