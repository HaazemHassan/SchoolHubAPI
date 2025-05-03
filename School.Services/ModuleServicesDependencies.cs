using Microsoft.Extensions.DependencyInjection;
using School.Services.Services;
using School.Services.ServicesContracts;

namespace School.Services
{
    public static class ModuleServicesDependencies
    {
        public static IServiceCollection AddServicesDepenedencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
