using School.Data.Entities;
using System.Linq.Expressions;

namespace School.Services.ServicesContracts
{
    public interface IDepartmentService
    {

        public Task<Department?> GetDepartmentDetailedByIdAsync(int id);
        public Task<Department?> GetDepartment(Expression<Func<Department, bool>> predicate);
        public Task<bool> IsDepartmentExist(Expression<Func<Department, bool>> predicate);
    }
}
