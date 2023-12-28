using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProniaOnion.Application.Abstraction.Services;
using A= ProniaOnion.Infrastructure.Implementations;
using System.Text;

namespace ProniaOnion.Infrastructure.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastuctureServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer= true,
                    ValidateAudience=true,
                    ValidateIssuerSigningKey=true,

                    ValidIssuer = config["Jwt:Issuer"],
                    ValidAudience = config["Jwt:Audience"],
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecurityKey"])),
                    LifetimeValidator=(notBefore,expired,security,param)=> expired > DateTime.UtcNow 



                };
            });
            services.AddAuthorization();
            services.AddScoped<ITokenHandler, A.TokenHandler>();
            return services;
        }
    }
}
