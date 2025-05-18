using School.Data.Entities.IdentityEntities;
using System.Linq.Expressions;

namespace School.Services.ServicesContracts
{
    public interface IApplicationUserService
    {
        public Task<bool> IsUserExist(Expression<Func<ApplicationUser, bool>> predicate);
    }
}
