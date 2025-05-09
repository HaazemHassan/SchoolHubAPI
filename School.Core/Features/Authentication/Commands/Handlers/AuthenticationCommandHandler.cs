﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authentication.Commands.Models;
using School.Core.SharedResources;
using School.Data.Entities.IdentityEntities;
using School.Data.Helpers;
using School.Services.ServicesContracts;

namespace School.Core.Features.Authentication.Commands.Handlers
{
    public class AuthenticationCommandHandler : ResponseHandler, IRequestHandler<SignInCommand, Response<JwtResult>>,
                                                    IRequestHandler<RefreshTokenCommand, Response<JwtResult>>
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

        public async Task<Response<JwtResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var userFromDb = await _userManager.FindByNameAsync(request.Username);
            if (userFromDb is null)
                return Unauthorized<JwtResult>("Invalid username or password");

            bool isAuthenticated = await _userManager.CheckPasswordAsync(userFromDb, request.Password);
            if (!isAuthenticated)
                return Unauthorized<JwtResult>("Invalid username or password");

            JwtResult token = await _authenticationService.AuthenticateAsync(userFromDb);
            return Success(token);

        }


        public async Task<Response<JwtResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                JwtResult jwtResult = await _authenticationService.ReAuthenticateAsync(request.RefreshToken, request.AccessToken);
                return Success(jwtResult);

            }
            catch (Exception e)
            {
                return Unauthorized<JwtResult>();
            }

        }
    }
}