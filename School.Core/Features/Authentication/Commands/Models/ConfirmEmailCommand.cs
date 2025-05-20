using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Authentication.Commands.Models
{
    public class ConfirmEmailCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string Code { get; set; }
    }
}
