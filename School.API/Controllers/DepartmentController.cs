using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.API.Bases;
using School.Core.Bases;
using School.Core.Features.Departments.Queries.Models;
using School.Core.Features.Departments.Queries.Results;

namespace School.API.Controllers
{

    public class DepartmentController : CustomControllerBase
    {
        public DepartmentController(IMediator mediator) : base(mediator) { }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDepartmentById([FromRoute] int id, int StudentPageNumber = 1, int StudentPageSize = 10)
        {
            Response<GetDepartmentByIdResponse>? response =
                await mediator.Send(new GetDepartmentDetailedByIdQuery(id, StudentPageNumber, StudentPageSize));

            return NewResult(response);
        }

    }
}
