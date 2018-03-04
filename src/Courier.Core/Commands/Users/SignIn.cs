using System;

namespace Courier.Core.Commands.Users
{
    public class SignIn : ICommand
    {
        public Guid UserId { get; set; }
        public string Email { get; }
        public string Password { get; }

        public SignIn(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}