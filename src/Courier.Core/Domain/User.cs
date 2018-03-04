using System;
using Microsoft.AspNetCore.Identity;

namespace Courier.Core.Domain
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string PasswordHash { get; private set; }
        public Address Address { get; private set; }
        public Role Role { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public User(Guid id, string email, string firstName, string lastName, Role role = Role.User)
        {
            Id = id;
            Email = email.ToLowerInvariant();
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Invalid password.", nameof(password));
            }
            PasswordHash = passwordHasher.HashPassword(this, password);
        }
    }
}