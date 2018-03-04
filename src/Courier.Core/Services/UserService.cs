using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courier.Core.Domain;
using Courier.Core.Dto;
using Microsoft.AspNetCore.Identity;

namespace Courier.Core.Services
{
    public class UserService : IUserService
    {
        private static readonly ISet<User> _users = new HashSet<User>();
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtService _jwtService;
        
        public UserService(IPasswordHasher<User> passwordHasher, IJwtService jwtService)
        {
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
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

        public async Task<JsonWebTokenDto> SignInAsync(string email, string password)
        {
            var user = _users.SingleOrDefault(x => x.Email == email.ToLowerInvariant());
            if (user == null || !user.ValidatePassword(password, _passwordHasher))
            {
                throw new Exception("Invalid credentials.");
            }
            await Task.CompletedTask;
            
            return _jwtService.CreateToken(user.Id, user.Role.ToString());
        }

        public async Task ChangePasswordAsync(Guid id, string currentPassword, string newPassword)
        {
            throw new NotImplementedException();
        }
    }
}