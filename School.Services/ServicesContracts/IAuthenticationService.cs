using School.Data.Entities.IdentityEntities;
using School.Data.Helpers.Authentication;
using School.Services.Bases;
using System.Security.Claims;

namespace School.Services.ServicesContracts
{
    public interface IAuthenticationService
    {
        public Task<JwtResult?> AuthenticateAsync(ApplicationUser user, DateTime? refreshTokenExpDate = null);
        public ClaimsPrincipal? GetPrincipalFromAcessToken(string token, bool validateLifetime = true);
        public Task<JwtResult?> ReAuthenticateAsync(string refreshToken, string accessToken);
        public Task<ServiceOpertaionResult> SendResetPasswordCodeAsync(string email);
        public Task<JwtResult?> VerifyResetPasswordCodeAsync(string email, string code);
    }
}
