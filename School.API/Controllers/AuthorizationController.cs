using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.API.Bases;
using School.Core.Features.Authorization.Commands.Models;

namespace School.API.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AuthorizationController : CustomControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }




        [HttpPost("role/create")]
        public async Task<IActionResult> createRole([FromForm] AddRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
    }
}
