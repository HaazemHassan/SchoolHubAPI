using MediatR;
using School.Core.Bases;
using School.Data.Helpers.Authorization;

namespace School.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserClaimsCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public List<UserClaim> Claims { get; set; }
    }
}
