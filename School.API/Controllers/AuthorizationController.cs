using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.API.Bases;
using School.Core.Features.Authorization.Commands.Models;
using School.Core.Features.Authorization.Queries.Models;

namespace School.API.Controllers
{

    //[Authorize(Roles = "Admin")]
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

        [HttpGet("user-claims/{userId}")]
        public async Task<IActionResult> GetUserClaims([FromRoute] int userId)
        {
            var result = await _mediator.Send(new GetUserClaimsQuery { UserId = userId });
            return NewResult(result);
        }

        [HttpPost("user-claims/{userId}")]
        public async Task<IActionResult> UpdateUserClaims([FromRoute] int userId, [FromBody] UpdateUserClaimsCommand command)
        {
            command.UserId = userId;
            var result = await _mediator.Send(command);
            return NewResult(result);
        }
    }
}
