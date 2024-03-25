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
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Domain.Entites;
using Persistence;
using Microsoft.AspNetCore.Identity;
using Application.Common.Interfaces.Payment;
using Infrastructure.Repositories.Payment;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,ConfigurationManager configuration)
        {
            services
          .AddAuth(configuration);

            services.AddSingleton<IDateTimeProvider, DatetimeProvider>();

            services.AddDbContext<ApplicationDbContext>();
            services.AddIdentity<User, Roles>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();

            return services;
        }

        public static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration configration)
        {
            var jwtSettings = new JwtSettings();
            configration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });



            return services;
        }
    }
}
