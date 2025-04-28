using School.Data.Entities;

namespace School.Services.ServicesContracts
{
    public interface IDepartmentService
    {

        public Task<Department?> GetDepartmentDetailedByIdAsync(int id);
    }
}
