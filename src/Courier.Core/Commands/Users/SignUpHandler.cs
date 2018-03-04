using System.Threading.Tasks;
using Courier.Core.Services;

namespace Courier.Core.Commands.Users
{
    public class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IUserService _userService;

        public SignUpHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(SignUp command)
        {
            await _userService.SignUpAsync(command.Id, command.Email, command.Password);
        }
    }
}