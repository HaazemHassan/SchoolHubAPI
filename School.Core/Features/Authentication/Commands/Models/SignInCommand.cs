using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Authentication.Commands.Models
{
    public class SignInCommand : IRequest<Response<string>>
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
