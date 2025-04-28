using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Context;
using School.Infrastructure.InfrastructureBases;
using School.Infrastructure.RepositoriesContracts;

namespace School.Infrastructure.Repositories
{
    public class SubjectRepository : GenericRepository<Subject>, ISubjectRepository
    {

        private readonly DbSet<Subject> _subjects;


        public SubjectRepository(AppDbContext context) : base(context)
        {
            _subjects = context.Set<Subject>();
        }


    }
}
