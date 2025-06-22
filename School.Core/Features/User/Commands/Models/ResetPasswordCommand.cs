using MediatR;
using School.Core.Bases;

namespace School.Core.Features.User.Commands.Models
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
