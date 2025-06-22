using MediatR;
using School.Core.Bases;
using School.Data.Helpers.Authentication;

namespace School.Core.Features.Authentication.Commands.Models
{
    public class VerifyResetPasswordCodeCommand : IRequest<Response<JwtResult>>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
