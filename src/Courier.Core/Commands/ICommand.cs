using System;

namespace Courier.Core.Commands
{
    public interface ICommand
    {
        Guid UserId { get; set; }
    }
}