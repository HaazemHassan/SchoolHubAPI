using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities.IdentityEntities;
using School.Data.Enums;
using School.Infrastructure.Context;
using School.Services.Bases;
using School.Services.ServicesContracts;
using System.Linq.Expressions;

namespace School.Services.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailsService;
        private readonly IUrlHelper _urlHelper;



        public ApplicationUserService(UserManager<ApplicationUser> userManager, AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IEmailService emailsService, IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _emailsService = emailsService;
            _urlHelper = urlHelper;
        }


        public async Task<bool> IsUserExist(Expression<Func<ApplicationUser, bool>> predicate)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(predicate);
            return user is null ? false : true;
        }

        public async Task<ServiceOpertaionResult> AddApplicationUser(ApplicationUser user, string password)
        {

            if (user is null || password is null)
                return ServiceOpertaionResult.Failed;


            await using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (await IsUserExist(x => x.Email == user.Email || x.UserName == user.UserName))
                        return ServiceOpertaionResult.AlreadyExists;

                    var createResult = await _userManager.CreateAsync(user, password);

                    if (!createResult.Succeeded)
                        return ServiceOpertaionResult.Failed;

                    var addToRoleresult = await _userManager.AddToRoleAsync(user, ApplicationUserRole.User.ToString());
                    if (!addToRoleresult.Succeeded)
                        return ServiceOpertaionResult.Failed;

                    var succedded = await SendConfirmationEmailAsync(user);
                    if (!succedded)
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

        public async Task<bool> SendConfirmationEmailAsync(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var resquestAccessor = _httpContextAccessor.HttpContext.Request;
            var confrimEmailActionContext = new UrlActionContext
            {
                Action = "ConfirmEmail",
                Controller = "Authentication",
                Values = new { UserId = user.Id, Code = code }
            };
            var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host + _urlHelper.Action(confrimEmailActionContext);
            var message = $"To Confirm Email Click Link: {returnUrl}";
            var sendResult = await _emailsService.SendEmail(user.Email, message);
            return sendResult;
        }
    }
}
