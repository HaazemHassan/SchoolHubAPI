using School.Data.Helpers.Authorization;
using School.Services.Bases;

namespace School.Services.ServicesContracts
{
    public interface IAuthorizationService
    {
        public Task<ServiceOpertaionResult> AddRoleAsync(string name);
        public Task<List<UserClaim>?> GetUserClaims(int userId);
        public Task<ServiceOpertaionResult> UpdateUserClaims(int userId, List<UserClaim> newClaims);

    }
}
