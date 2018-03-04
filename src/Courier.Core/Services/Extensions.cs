using System.Text;
using Courier.Core.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Courier.Core.Services
{
    public static class Extensions
    {
        public static void AddJwt(this IServiceCollection services)
        {
            IConfiguration configuration;
            using(var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            var section = configuration.GetSection("jwt");
            // services.Configure<JwtOptions>(section);
            var options = new JwtOptions();
            section.Bind(options);
            services.AddAuthentication()
                .AddJwtBearer(cfg => 
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                        ValidIssuer = options.Issuer,
                        ValidateAudience = false,
                        ValidateLifetime = true
                    };
                });
        }
    }
}