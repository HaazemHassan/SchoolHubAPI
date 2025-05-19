using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.API.Bases;
using School.Core.Features.Email.Commands.Models;

namespace School.API.Controllers
{

    [Authorize(Roles = "Admin")]
    public class EmailController : CustomControllerBase
    {
        public EmailController(IMediator mediator) : base(mediator) { }


        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailCommand command)
        {
            var response = await mediator.Send(command);
            return NewResult(response);
        }

    }
}
