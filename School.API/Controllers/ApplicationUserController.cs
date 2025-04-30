using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.API.Bases;
using School.Core.Features.User.Commands.Models;
using School.Core.Features.User.Queries.Models;

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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetUsersPaginatedQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get([FromRoute] GetUserByIdQuery query)
        {
            var result = await mediator.Send(query);
            return NewResult(result);
        }
    }
}
