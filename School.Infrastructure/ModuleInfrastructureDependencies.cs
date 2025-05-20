using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Data.Entities.IdentityEntities;
using School.Infrastructure.Context;
using School.Infrastructure.InfrastructureBases;
using School.Infrastructure.Repositories;
using School.Infrastructure.RepositoriesContracts;

namespace School.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastractureDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            DbContextServiceConfiguations(services, configuration);
            RepositoryServiceConfiguations(services);
            IdentityServiceConfiguations(services);
            return services;
        }


        private static IServiceCollection DbContextServiceConfiguations(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
             {
                 options.UseSqlServer(configuration["ConnectionStrings:SqlServerConStr"]);
             });

            return services;
        }


        private static IServiceCollection IdentityServiceConfiguations(IServiceCollection services)
        {

            services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
            {
                // Password settings.
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredLength = 3;
                option.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.AllowedForNewUsers = true;

                // User settings.
                option.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                option.User.RequireUniqueEmail = true;
                option.SignIn.RequireConfirmedEmail = true;


            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            return services;
        }


        private static IServiceCollection RepositoryServiceConfiguations(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IInstructorRepository, InsturctorRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            return services;
        }
    }


}

