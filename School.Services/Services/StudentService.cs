using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Data.Helpers;
using School.Infrastructure.RepositoriesContracts;
using School.Services.Bases;
using School.Services.ServicesContracts;
using System.Linq.Expressions;

namespace School.Services.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IDepartmentService _departmentService;

        public StudentService(IStudentRepository studentRepository, IDepartmentService departmentService)
        {
            _studentRepository = studentRepository;
            _departmentService = departmentService;
        }

        public async Task<ServiceOpertaionResult> AddAsync(Student studentToAdd)
        {
            //You already added this in  School.Core.Features.Students.Commands.Validators
            //if (await IsNameExists(studentToAdd.Name))
            //    return ServiceOpertaionResult.Exists;

            if (studentToAdd.DID is not null)
            {
                Department? dept = await _departmentService.GetDepartment(x => x.DID == studentToAdd.DID);
                if (dept is null)
                    return ServiceOpertaionResult.DependencyNotExist;
            }



            await _studentRepository.AddAsync(studentToAdd);
            return ServiceOpertaionResult.Succeeded;

        }

        public Task<Student?> GetStudentAsync(Expression<Func<Student, bool>> predicate)
        {


            return _studentRepository.GetTableNoTracking().
                Include(x => x.Department)
                .FirstOrDefaultAsync(predicate);

        }


        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _studentRepository.GetStudentsAsync();
        }

        public async Task<bool> IsNameExistsAsync(string name)
        {
            return await _studentRepository.GetTableNoTracking().AnyAsync(x => x.Name.Equals(name));
        }

        public async Task<bool> IsNameExistsExcludeSelf(string name, int studentId)
        {
            return await _studentRepository.GetTableNoTracking().
                AnyAsync(x => x.Name.Equals(name) && x.StudID != studentId);
        }

        public async Task<ServiceOpertaionResult> UpdateAsync(Student studentToUpdate)
        {
            var studentFromDb = await _studentRepository.GetByIdAsync(studentToUpdate.StudID);

            if (studentFromDb is null)
                return ServiceOpertaionResult.NotExist;

            if (!string.IsNullOrWhiteSpace(studentToUpdate.Name))
                studentFromDb.Name = studentToUpdate.Name;

            if (!string.IsNullOrWhiteSpace(studentToUpdate.Address))
                studentFromDb.Address = studentToUpdate.Address;

            if (!string.IsNullOrWhiteSpace(studentToUpdate.Phone))
                studentFromDb.Phone = studentToUpdate.Phone;

            studentFromDb.DID = studentToUpdate.DID ?? studentFromDb.DID;


            await _studentRepository.SaveChangesAsync();
            return ServiceOpertaionResult.Succeeded;

        }

        public async Task<ServiceOpertaionResult> DeleteAsync(int id)
        {
            Student? student = await _studentRepository.GetAsync(x => x.StudID == id);

            if (student is null)
                return ServiceOpertaionResult.NotExist;


            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                //other database operations
                // ...
                // ...
                // ...
                await trans.CommitAsync();
                return ServiceOpertaionResult.Succeeded;
            }
            catch (SqlException ex)
            {
                await trans.RollbackAsync();
                throw;
            }
        }


        public IQueryable<Student> GetQueryable(StudentOrderingEnum? orderBy, string? search)
        {
            var query = _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(x => x.Name.Contains(search) || (x.Address != null && x.Address.Contains(search)));

            if (orderBy is not null)
            {
                switch (orderBy)
                {
                    case StudentOrderingEnum.Name:
                        query = query.OrderBy(x => x.Name);
                        break;
                    case StudentOrderingEnum.Address:
                        query = query.OrderBy(x => x.Address);
                        break;
                    case StudentOrderingEnum.DepartmentName:
                        query = query.OrderBy(x => x.Department.DName);
                        break;
                    default:
                        query = query.OrderBy(x => x.StudID);
                        break;
                }
            }

            return query;
        }

        public IQueryable<Student> GetQueryable(Expression<Func<Student, bool>>? predicate)
        {
            if (predicate is null)
                return _studentRepository.GetTableNoTracking();
            return _studentRepository.GetTableNoTracking().Where(predicate);
        }
    }
}
