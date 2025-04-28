using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Context;
using School.Infrastructure.InfrastructureBases;
using School.Infrastructure.RepositoriesContracts;

namespace School.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {

        private readonly DbSet<Department> _departments;



        public DepartmentRepository(AppDbContext context) : base(context)
        {
            _departments = context.Set<Department>();
        }

    }
}
