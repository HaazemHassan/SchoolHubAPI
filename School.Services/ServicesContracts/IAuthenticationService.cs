using School.Data.Entities.IdentityEntities;
using School.Data.Helpers.Authentication;
using System.Security.Claims;

namespace School.Services.ServicesContracts
{
    public interface IAuthenticationService
    {
        public Task<JwtResult> AuthenticateAsync(ApplicationUser user);
        public ClaimsPrincipal? GetPrincipalFromAcessToken(string token, bool validateLifetime = true);
        public Task<JwtResult> ReAuthenticateAsync(string refreshToken, string accessToken);
    }
}
