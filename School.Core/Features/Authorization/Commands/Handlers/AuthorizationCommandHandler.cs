using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Commands.Models;
using School.Core.SharedResources;
using School.Data.Entities.IdentityEntities;
using School.Services.Bases;
using School.Services.ServicesContracts;

namespace School.Core.Features.Authorization.Commands.Handlers
{
    public class AuthorizationCommandHandler : ResponseHandler, IRequestHandler<AddRoleCommand, Response<string>>

    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationCommandHandler(IStringLocalizer<Resources> localizer,
                      IMapper mapper,
                      UserManager<ApplicationUser> userManager,
                      SignInManager<ApplicationUser> signInManager,
                      IAuthorizationService authorizationService) : base(localizer)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authorizationService = authorizationService;
        }


        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {

            var result = await _authorizationService.AddRoleAsync(request.Name);

            if (result == ServiceOpertaionResult.AlreadyExists)
                return Conflict<string>();

            if (result == ServiceOpertaionResult.Succeeded)
                return Success<string>(null);

            return BadRequest<string>();
        }
    }
}