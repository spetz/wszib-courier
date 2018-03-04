using System;
using System.Threading.Tasks;
using Courier.Core.Domain;

namespace Courier.Core.Services
{
    public interface IUserService
    {
        Task SignUpAsync(Guid id, string email, string password, Role role = Role.User);
        Task SignInAsync(string email, string password);
        Task ChangePasswordAsync(Guid id, string currentPassword, string newPassword);
    }
}