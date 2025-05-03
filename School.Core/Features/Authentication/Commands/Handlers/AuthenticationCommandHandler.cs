using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authentication.Commands.Models;
using School.Core.SharedResources;
using School.Data.Entities.IdentityEntities;
using School.Services.ServicesContracts;

namespace School.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler, IRequestHandler<SignInCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationCommandHandler(IStringLocalizer<Resources> localizer,
                      IMapper mapper,
                      UserManager<ApplicationUser> userManager,
                      SignInManager<ApplicationUser> signInManager
,
                      IAuthenticationService authenticationService) : base(localizer)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
        }

        public async Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var userFromDb = await _userManager.FindByNameAsync(request.Username);
            if (userFromDb is null)
                return Unauthorized<string>("Invalid username or password");

            bool isAuthenticated = await _userManager.CheckPasswordAsync(userFromDb, request.Password);
            if (!isAuthenticated)
                return Unauthorized<string>("Invalid username or password");

            string token = await _authenticationService.GetJwtAsync(userFromDb);
            return Success(token);

        }
    }
}