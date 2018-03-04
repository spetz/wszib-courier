using System;
using System.Threading.Tasks;
using Courier.Core.Services;

namespace Courier.Core.Commands.Users
{
    public class SignInHandler : ICommandHandler<SignIn>
    {
        private readonly IUserService _userService;

        public SignInHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(SignIn command)
        {
            var jwt = await _userService.SignInAsync(command.Email, command.Password);
        }
    }
}