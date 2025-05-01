using MediatR;
using School.Core.Bases;

namespace School.Core.Features.User.Commands.Models
{
    public class DeleteUserByIdCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

    }
}
