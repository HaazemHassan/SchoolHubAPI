using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.User.Commands.Models;
using School.Core.SharedResources;
using School.Data.Entities.IdentityEntities;

namespace School.Core.Features.User.Commands.Handlers
{
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserCommandHandler(IStringLocalizer<Resources> localizer, IMapper mapper, UserManager<ApplicationUser> userManager) : base(localizer)
        {
            _mapper = mapper;
            _userManager = userManager;
        }


        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser? currentUserByEmail = await _userManager.FindByEmailAsync(request.Email);
            if (currentUserByEmail is not null)
                return Conflict<string>("This email already used");

            ApplicationUser? currentUserByUserName = await _userManager.FindByNameAsync(request.UserName);
            if (currentUserByUserName is not null)
                return Conflict<string>("This username already used");

            var userMapped = _mapper.Map<ApplicationUser>(request);

            var result = await _userManager.CreateAsync(userMapped, request.Password);

            if (!result.Succeeded)
                return BadRequest<string>(result.Errors.FirstOrDefault()?.Description);

            return Created();
        }
    }
}
