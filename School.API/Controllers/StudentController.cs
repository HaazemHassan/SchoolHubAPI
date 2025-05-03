using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.API.Bases;
using School.Core.Bases;
using School.Core.Features.Students.Commands.Models;
using School.Core.Features.Students.Queries.Models;
using School.Core.Features.Students.Queries.Results;

namespace School.API.Controllers
{
    [Authorize]
    public class StudentController : CustomControllerBase
    {

        public StudentController(IMediator mediator) : base(mediator) { }


        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var response = await mediator.Send(new GetStudentsQuery());
            return NewResult(response);
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetStudentsPaginated([FromQuery] GetStudentsPaginatedQuery query)
        {

            var response = await mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetStudentById(int id)
        {
            Response<GetStudentByIdResponse>? response = await mediator.Send(new GetStudentByIdQuery(id));
            return NewResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddStudentCommand command)
        {
            Response<string>? response = await mediator.Send(command);
            //return NewResult(response);

            //HOW TO GET ID ?? (think later)
            return CreatedAtAction(nameof(GetStudentById), new { id = 0 }, response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateStudentCommand command)
        {
            Response<string>? response = await mediator.Send(command);

            return NewResult(response);
        }

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteStudentCommand command)
        {
            Response<string>? response = await mediator.Send(command);

            return NewResult(response);
        }
    }
}

