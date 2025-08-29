using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using School.Data.Entities.IdentityEntities;
using School.Services.ServicesContracts;
using System.Security.Claims;

namespace School.Services.Services
{
    public class CurrentApplicationUserService : ICurrentApplicationUserService
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly UserManager<ApplicationUser> _userManager;

        public CurrentApplicationUserService(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
                throw new InvalidOperationException("User is not authenticated or user ID is not valid.");

            return parsedUserId;
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User) ??
                   throw new InvalidOperationException("User is not authenticated or user does not exist.");

        }
    }
}
