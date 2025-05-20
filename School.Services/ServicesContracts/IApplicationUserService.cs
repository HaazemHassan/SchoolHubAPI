using School.Data.Entities.IdentityEntities;
using School.Services.Bases;
using System.Linq.Expressions;

namespace School.Services.ServicesContracts
{
    public interface IApplicationUserService
    {
        public Task<bool> IsUserExist(Expression<Func<ApplicationUser, bool>> predicate);
        public Task<ServiceOpertaionResult> AddApplicationUser(ApplicationUser user, string password);
        public Task<bool> SendConfirmationEmailAsync(ApplicationUser user);


    }
}
