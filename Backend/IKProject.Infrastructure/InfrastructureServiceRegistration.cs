using IKProject.Application.Interfaces.Token;
using IKProject.Infrastructure.TokenService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace IKProject.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ITokenServices, TokenServices>();
            services.Configure<TokenSettings>(configuration.GetSection("Token"));

            var tokenSettings = configuration.GetSection("Token").Get<TokenSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenSettings.Issuer,
                    ValidAudience = tokenSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecurityKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}



