using School.Data.Entities.IdentityEntities;

namespace School.Services.ServicesContracts
{
    public interface ICurrentApplicationUserService
    {

        public int GetCurrentUserId();
        public Task<ApplicationUser> GetCurrentUser();
    }
}
