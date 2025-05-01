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
    public class UserCommandHandler : ResponseHandler, IRequestHandler<AddUserCommand, Response<string>>,
                                                       IRequestHandler<UpdateUserCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public UserCommandHandler(IStringLocalizer<Resources> localizer, IMapper mapper, UserManager<ApplicationUser> userManager, IPasswordHasher<ApplicationUser> passwordHasher) : base(localizer)
        {
            _mapper = mapper;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
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

        public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser? userFromDb = await _userManager.FindByIdAsync(request.Id.ToString());
            if (userFromDb is null)
                return NotFound<string>();

            bool isPasswordValid = await _userManager.CheckPasswordAsync(userFromDb, request.Password);
            if (!isPasswordValid)
                return Unauthorized<string>();

            var userMapped = _mapper.Map(request, userFromDb);
            var updateResult = await _userManager.UpdateAsync(userMapped);
            if (updateResult.Succeeded)
                return Updated<string>();

            return BadRequest<string>(updateResult?.Errors?.FirstOrDefault()?.Description);


        }
    }
}
