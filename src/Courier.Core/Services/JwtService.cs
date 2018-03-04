using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Courier.Core.Dto;
using Courier.Core.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Courier.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _options;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly SecurityKey _securityKey;
        private readonly SigningCredentials _signingCredentials;

        public JwtService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
        }

        public JsonWebTokenDto CreateToken(Guid userId, string role)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            var expires = now.AddMinutes(_options.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new JsonWebTokenDto
            {
                AccessToken = token
            };
        }
    }
}