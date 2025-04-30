using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.User.Queries.Models;
using School.Core.Features.User.Queries.Results;
using School.Core.SharedResources;
using School.Core.Wrappers;
using School.Data.Entities.IdentityEntities;

namespace School.Core.Features.User.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
                                    IRequestHandler<GetUsersPaginatedQuery, PaginatedResult<GetUsersPaginatedResponse>>,
                                    IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;



        public UserQueryHandler(UserManager<ApplicationUser> userManager, IStringLocalizer<Resources> localizer, IMapper mapper) : base(localizer)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetUsersPaginatedResponse>> Handle(GetUsersPaginatedQuery request, CancellationToken cancellationToken)
        {
            var usersQuerable = _userManager.Users;
            var usersPaginatedResult = await _mapper.ProjectTo<GetUsersPaginatedResponse>(usersQuerable)
                                .ToPaginatedResultAsync(request.PageNumber, request.PageSize);
            return usersPaginatedResult;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                return BadRequest<GetUserByIdResponse>();

            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null)
                return NotFound<GetUserByIdResponse>();

            var userResponse = _mapper.Map<GetUserByIdResponse>(user);
            return Success(userResponse);
        }
    }
}
