using Microsoft.AspNetCore.Identity;
using School.Data.Entities.IdentityEntities;
using School.Data.Helpers.Authorization;
using School.Infrastructure.Context;
using School.Services.Bases;
using School.Services.ServicesContracts;
using System.Security.Claims;

namespace School.Services.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _dbContext;

        public AuthorizationService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, AppDbContext dbContext)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _dbContext = dbContext;
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

        public async Task<List<UserClaim>?> GetUserClaims(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                return null;

            var userClaims = await _userManager.GetClaimsAsync(user);
            var response = new List<UserClaim>();

            foreach (var claim in ClaimsStore.Claims)
            {
                var userClaim = new UserClaim();
                userClaim.Type = claim.Type;
                if (userClaims.Any(x => x.Type == userClaim.Type))
                    userClaim.Value = true;

                response.Add(userClaim);
            }

            return response;

        }

        public async Task<ServiceOpertaionResult> UpdateUserClaims(int userId, List<UserClaim> newClaims)
        {
            if (newClaims == null || !newClaims.Any())
                return ServiceOpertaionResult.InvalidParameters;

            if (!newClaims.All(IsValidUserClaim))
                return ServiceOpertaionResult.NotExist;

            await using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(userId.ToString());
                    if (user is null)
                        return ServiceOpertaionResult.DependencyNotExist;

                    var currentUserClaims = await _userManager.GetClaimsAsync(user);
                    var removeClaimsResult = await _userManager.RemoveClaimsAsync(user, currentUserClaims);

                    if (!removeClaimsResult.Succeeded)
                        return ServiceOpertaionResult.Failed;

                    var claimsToAdd = newClaims.Where(x => x.Value == true)
                              .Select(x => new Claim(x.Type, x.Value.ToString()));

                    var addClaimsResult = await _userManager.AddClaimsAsync(user, claimsToAdd);

                    if (!addClaimsResult.Succeeded)
                        return ServiceOpertaionResult.Failed;

                    await transaction.CommitAsync();
                    return ServiceOpertaionResult.Succeeded;

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return ServiceOpertaionResult.Failed;
                }

            }
        }

        public bool IsValidUserClaim(UserClaim claim)
        {
            var allUserClaims = ClaimsStore.Claims;
            return allUserClaims.Any(x => x.Type == claim.Type);
        }

    }
}
