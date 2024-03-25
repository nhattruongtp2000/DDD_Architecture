using Application.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Authentication.Commands;
using Application.Authentication.Queries;
using MediatR;
using System.Reflection;
using FluentValidation;
using Application.Authentication.Behaviors;

namespace Application
{
    public static class DependencyInjectionRegister
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyInjectionRegister).Assembly);

            services.AddScoped(
          typeof(IPipelineBehavior<,>),
          typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
