using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Authorization.Queries.Models;
using School.Core.Features.Authorization.Queries.Results;
using School.Core.SharedResources;
using School.Services.ServicesContracts;

namespace School.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimsQueryHandler : ResponseHandler, IRequestHandler<GetUserClaimsQuery, Response<GetUserClaimsResponse>>

    {
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public ClaimsQueryHandler(IStringLocalizer<Resources> localizer,
                      IMapper mapper,
                      IAuthorizationService authorizationService) : base(localizer)
        {
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        public async Task<Response<GetUserClaimsResponse>> Handle(GetUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var claimsList = await _authorizationService.GetUserClaims(request.UserId);
            if (claimsList is null)
                return NotFound<GetUserClaimsResponse>("User not found");


            GetUserClaimsResponse response = new GetUserClaimsResponse
            {
                UserId = request.UserId,
                Claims = claimsList
            };

            return Success(response);
        }
    }
}