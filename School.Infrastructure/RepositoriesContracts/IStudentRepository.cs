using School.Data.Entities;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.RepositoriesContracts
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        public Task<List<Student>> GetStudentsAsync();
    }
}
