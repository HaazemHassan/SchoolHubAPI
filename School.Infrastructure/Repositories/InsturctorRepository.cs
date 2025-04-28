using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Context;
using School.Infrastructure.InfrastructureBases;
using School.Infrastructure.RepositoriesContracts;

namespace School.Infrastructure.Repositories
{
    public class InsturctorRepository : GenericRepository<Instructor>, IInstructorRepository
    {

        private readonly DbSet<Instructor> _instructors;


        public InsturctorRepository(AppDbContext context) : base(context)
        {
            _instructors = context.Set<Instructor>();
        }

    }
}
