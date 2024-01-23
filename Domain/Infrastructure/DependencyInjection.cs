using Application.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Services;
using Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Infrastructure.Services;
using Application.Common.Interfaces.Persistence;
using Infrastructure.Persistence;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DatetimeProvider>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
