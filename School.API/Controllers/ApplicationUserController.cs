using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.API.Bases;
using School.Core.Features.User.Commands.Models;
using School.Core.Features.User.Queries.Models;

public class ApplicationUserController : CustomControllerBase
{
    public ApplicationUserController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddUserCommand command)
    {
        var result = await mediator.Send(command);
        return NewResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetUsersPaginatedQuery query)
    {
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] GetUserByIdQuery query)
    {
        var result = await mediator.Send(query);
        return NewResult(result);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserCommand command)
    {
        command.Id = id;
        var result = await mediator.Send(command);
        return NewResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteUserByIdCommand command)
    {
        var result = await mediator.Send(command);
        return NewResult(result);
    }

    [HttpPatch("{id:int}/password")]
    public async Task<IActionResult> UpdatePassword([FromRoute] int id, [FromBody] ChangePasswordCommand command)
    {
        command.Id = id;
        var result = await mediator.Send(command);
        return NewResult(result);
    }
}