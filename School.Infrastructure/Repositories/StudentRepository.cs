using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Context;
using School.Infrastructure.InfrastructureBases;
using School.Infrastructure.RepositoriesContracts;

namespace School.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {

        private readonly DbSet<Student> _students;



        public StudentRepository(AppDbContext context) : base(context)
        {
            _students = context.Set<Student>();
        }

        public async Task<List<Student>> GetStudentsAsync()
        {

            return await _students.Include(s => s.Department).ToListAsync();
        }
    }
}
