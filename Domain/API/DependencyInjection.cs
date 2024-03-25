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
using API.Common.Mapping;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddSingleton<IMapper, Mapper>();

            return services;
        }
    }
}
