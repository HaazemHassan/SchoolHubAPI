﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using School.Core.Bases;
using System.Net;

namespace School.API.Bases
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomControllerBase : ControllerBase
    {

        protected IMediator mediator;

        public CustomControllerBase(IMediator mediator)
        {
            this.mediator = mediator;

        }


        public IActionResult NewResult<T>(Response<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case HttpStatusCode.Conflict:
                    return new ConflictObjectResult(response);
                default:
                    return new BadRequestObjectResult(response);
            }
        }


    }
}
