using System;

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

        public User(string email, string firstName, string lastName, Role role = Role.User)
        {
            Id = Guid.NewGuid();
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }
    }
}