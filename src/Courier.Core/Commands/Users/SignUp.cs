using System;

namespace Courier.Core.Commands.Users
{
    public class SignUp : ICommand
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Email { get; }
        public string Password { get; }

        public SignUp(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}