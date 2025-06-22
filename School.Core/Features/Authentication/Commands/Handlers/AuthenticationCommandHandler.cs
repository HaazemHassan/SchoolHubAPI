using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authentication.Commands.Models;
using School.Core.SharedResources;
using School.Data.Entities.IdentityEntities;
using School.Data.Helpers.Authentication;
using School.Services.Bases;
using School.Services.ServicesContracts;

namespace School.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler, IRequestHandler<SignInCommand, Response<JwtResult>>,
                                                    IRequestHandler<RefreshTokenCommand, Response<JwtResult>>,
                                                    IRequestHandler<ConfirmEmailCommand, Response<string>>,
                                                    IRequestHandler<ResendConfirmationEmailCommand, Response<string>>,
                                                    IRequestHandler<SendResetPasswordCodeCommand, Response<string>>,
                                                    IRequestHandler<VerifyResetPasswordCodeCommand, Response<JwtResult>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        private readonly IApplicationUserService _applicationUserService;

        public AuthenticationCommandHandler(IStringLocalizer<Resources> localizer,
                      IMapper mapper,
                      UserManager<ApplicationUser> userManager,
                      SignInManager<ApplicationUser> signInManager
,
                      IAuthenticationService authenticationService,
                      IApplicationUserService applicationUserService) : base(localizer)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationService = authenticationService;
            _applicationUserService = applicationUserService;
        }

        public async Task<Response<JwtResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var userFromDb = await _userManager.FindByNameAsync(request.Username);
            if (userFromDb is null)
                return Unauthorized<JwtResult>("Invalid username or password");

            bool isAuthenticated = await _userManager.CheckPasswordAsync(userFromDb, request.Password);
            if (!isAuthenticated)
                return Unauthorized<JwtResult>("Invalid username or password");

            if (!userFromDb.EmailConfirmed)
                return Unauthorized<JwtResult>("Please confirm your email first");

            JwtResult? token = await _authenticationService.AuthenticateAsync(userFromDb);
            return token is null ? BadRequest<JwtResult>("Something went wrong") : Success(token);

        }


        public async Task<Response<JwtResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                JwtResult? jwtResult = await _authenticationService.ReAuthenticateAsync(request.RefreshToken, request.AccessToken);
                return jwtResult is null ? Unauthorized<JwtResult>() : Success(jwtResult);

            }
            catch (Exception e)
            {
                return Unauthorized<JwtResult>();
            }

        }

        public async Task<Response<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _applicationUserService.ConfirmEmailAsync(request.UserId, request.Code);
            return result switch
            {
                ServiceOpertaionResult.Succeeded => Success(),
                ServiceOpertaionResult.NotExist => NotFound<string>(),
                _ => BadRequest<string>(),
            };

        }

        public async Task<Response<string>> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            // TODO: we should handle if old email.

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
                return NotFound<string>();
            var sentSuccesfully = await _applicationUserService.SendConfirmationEmailAsync(user);
            return sentSuccesfully ? Success() : BadRequest<string>();
        }

        public async Task<Response<string>> Handle(SendResetPasswordCodeCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.SendResetPasswordCodeAsync(request.Email);
            return result switch
            {
                ServiceOpertaionResult.Succeeded => Success(),
                ServiceOpertaionResult.NotExist => NotFound<string>(),
                _ => BadRequest<string>()
            };

        }

        public async Task<Response<JwtResult>> Handle(VerifyResetPasswordCodeCommand request, CancellationToken cancellationToken)
        {
            JwtResult? jwtResult = await _authenticationService.VerifyResetPasswordCodeAsync(request.Email, request.Code);
            if (jwtResult is null)
                return Unauthorized<JwtResult>("Invalid reset password code or email");

            return Success(jwtResult);

        }
    }
}