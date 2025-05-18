using School.Data.Entities;
using School.Data.Enums;
using School.Services.Bases;
using System.Linq.Expressions;

namespace School.Services.ServicesContracts
{
    public interface IStudentService
    {
        public Task<List<Student>> GetStudentsAsync();
        public Task<Student?> GetStudentAsync(Expression<Func<Student, bool>> predicate);
        public Task<ServiceOpertaionResult> AddAsync(Student studentToAdd);
        public Task<bool> IsNameExistsAsync(string name);
        public Task<bool> IsNameExistsExcludeSelf(string name, int Id);
        public Task<ServiceOpertaionResult> UpdateAsync(Student studentToUpdate);
        public Task<ServiceOpertaionResult> DeleteAsync(int Id);
        public IQueryable<Student> GetQueryable(StudentOrderingEnum? orderBy = null, string? search = null);
        public IQueryable<Student> GetQueryable(Expression<Func<Student, bool>>? predicate);
    }
}
