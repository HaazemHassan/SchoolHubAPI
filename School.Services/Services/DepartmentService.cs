using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.RepositoriesContracts;
using School.Services.ServicesContracts;
using System.Linq.Expressions;

namespace School.Services.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task<Department?> GetDepartment(Expression<Func<Department, bool>> predicate)
        {
            return _departmentRepository.GetAsync(predicate);
        }

        public async Task<Department?> GetDepartmentDetailedByIdAsync(int id)
        {
            var result = await _departmentRepository.GetTableNoTracking()
                                      .Where(x => x.DID == id)
                                      .Include(x => x.Supervisor)
                                      .Include(x => x.Instructors)
                                      .Include(x => x.DepartmentSubjects).ThenInclude(ds => ds.Subject)
                                      .FirstOrDefaultAsync();
            return result;
        }

        public Task<bool> IsDepartmentExist(Expression<Func<Department, bool>> predicate)
        {
            return _departmentRepository.AnyAsync(predicate);
        }
    }
}
