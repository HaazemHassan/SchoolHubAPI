using MediatR;
using School.Core.Features.User.Queries.Results;
using School.Core.Wrappers;

namespace School.Core.Features.User.Queries.Models
{
    public class GetUsersPaginatedQuery : IRequest<PaginatedResult<GetUsersPaginatedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
