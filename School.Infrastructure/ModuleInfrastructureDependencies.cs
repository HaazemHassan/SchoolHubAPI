using Microsoft.Extensions.DependencyInjection;
using School.Infrastructure.InfrastructureBases;
using School.Infrastructure.Repositories;
using School.Infrastructure.RepositoriesContracts;

namespace School.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastractureDepenedencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IInstructorRepository, InsturctorRepository>();

            return services;
        }
    }
}
