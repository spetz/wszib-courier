using System;
using Courier.Core.Dto;

namespace Courier.Core.Services
{
    public interface IJwtService
    {
        JsonWebTokenDto CreateToken(Guid userId, string role);
    }
}