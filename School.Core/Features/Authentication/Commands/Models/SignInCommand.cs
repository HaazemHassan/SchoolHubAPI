using MediatR;
using School.Core.Bases;
using School.Data.Helpers;

namespace School.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<JwtResult>>
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
