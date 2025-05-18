using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Commands.Models;
using School.Core.SharedResources;
using School.Services.Bases;
using School.Services.ServicesContracts;

namespace School.Core.Features.Authorization.Commands.Handlers
{
    public class ClaimCommandHandler : ResponseHandler, IRequestHandler<UpdateUserClaimsCommand, Response<string>>

    {
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public ClaimCommandHandler(IStringLocalizer<Resources> localizer,
                      IMapper mapper,
                      IAuthorizationService authorizationService) : base(localizer)
        {
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var updateResult = await _authorizationService.UpdateUserClaims(request.UserId, request.Claims);

            return updateResult switch
            {
                ServiceOpertaionResult.Succeeded => Success(),
                ServiceOpertaionResult.NotExist => NotFound<string>("One or more claim does not exist"),
                ServiceOpertaionResult.DependencyNotExist => NotFound<string>("User not found"),
                _ => BadRequest<string>()
            };
        }
    }
}