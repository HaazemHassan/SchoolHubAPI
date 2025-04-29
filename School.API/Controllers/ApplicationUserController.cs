using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.API.Bases;
using School.Core.Features.User.Commands.Models;

namespace School.API.Controllers
{

    public class ApplicationUserController : CustomControllerBase
    {

        public ApplicationUserController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        public async Task<IActionResult> Register(AddUserCommand command)
        {
            var result = await mediator.Send(command);
            return NewResult(result);
        }
    }
}
