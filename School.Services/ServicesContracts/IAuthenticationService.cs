using School.Data.Entities.IdentityEntities;

namespace School.Services.ServicesContracts
{
    public interface IAuthenticationService
    {
        public Task<string> GetJwtAsync(ApplicationUser user);
    }
}
