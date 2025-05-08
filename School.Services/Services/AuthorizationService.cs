using Microsoft.AspNetCore.Identity;
using School.Data.Entities.IdentityEntities;
using School.Services.Bases;
using School.Services.ServicesContracts;

namespace School.Services.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AuthorizationService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ServiceOpertaionResult> AddRoleAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return ServiceOpertaionResult.InvalidParameters;

            var role = await _roleManager.FindByNameAsync(name);
            if (role is not null)
                return ServiceOpertaionResult.AlreadyExists;

            var result = await _roleManager.CreateAsync(new ApplicationRole(name));

            if (result.Succeeded)

                return ServiceOpertaionResult.Succeeded;

            return ServiceOpertaionResult.Failed;
        }
    }
}
