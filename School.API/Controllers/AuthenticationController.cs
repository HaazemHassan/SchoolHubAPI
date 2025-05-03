using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.API.Bases;
using School.Core.Features.Authentication.Commands.Models;

namespace School.API.Controllers
{

    public class AuthenticationController : CustomControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(SignInCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
    }
}
