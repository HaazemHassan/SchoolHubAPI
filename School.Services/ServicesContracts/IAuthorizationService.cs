using School.Services.Bases;

namespace School.Services.ServicesContracts
{
    public interface IAuthorizationService
    {
        public Task<ServiceOpertaionResult> AddRoleAsync(string name);

    }
}
