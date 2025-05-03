using School.Data.Entities.IdentityEntities;
using School.Data.Helpers;

namespace School.Services.ServicesContracts
{
    public interface IAuthenticationService
    {
        public Task<JwtResult> GenerateJwtAsync(ApplicationUser user);
    }
}
