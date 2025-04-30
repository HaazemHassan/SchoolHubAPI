using MediatR;
using School.Core.Bases;
using School.Core.Features.User.Queries.Results;

namespace School.Core.Features.User.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<GetUserByIdResponse>>
    {
        public int Id { get; set; }
    }
}
