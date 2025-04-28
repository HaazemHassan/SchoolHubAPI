using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using School.Core.Behaviour;
using System.Reflection;

namespace School.Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDepenedencies(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //fluent validaion services
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            return services;
        }
    }
}
