using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courier.Core.Domain;
using Microsoft.AspNetCore.Identity;

namespace Courier.Core.Services
{
    public class UserService : IUserService
    {
        private static readonly ISet<User> _users = new HashSet<User>();
        private readonly IPasswordHasher<User> _passwordHasher;
        
        public UserService(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task SignUpAsync(Guid id, string email, string password, Role role = Role.User)
        {
            var user = _users.SingleOrDefault(x => x.Email == email.ToLowerInvariant());
            if (user != null)
            {
                throw new Exception($"Email: '{email}' is already in use.");
            }
            user = new User(id, email, null, null, role);
            user.SetPassword(password, _passwordHasher);
            _users.Add(user);
            await Task.CompletedTask;
        }

        public async Task SignInAsync(string email, string password)
        {
            var user = _users.SingleOrDefault(x => x.Email == email.ToLowerInvariant());
            if (user == null || !user.ValidatePassword(password, _passwordHasher))
            {
                throw new Exception("Invalid credentials.");
            }
            await Task.CompletedTask;
        }

        public async Task ChangePasswordAsync(Guid id, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}