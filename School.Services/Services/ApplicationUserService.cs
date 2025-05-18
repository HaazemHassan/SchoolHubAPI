using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities.IdentityEntities;
using School.Services.ServicesContracts;
using System.Linq.Expressions;

namespace School.Services.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public ApplicationUserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> IsUserExist(Expression<Func<ApplicationUser, bool>> predicate)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(predicate);
            return user is null ? false : true;
        }
    }
}
