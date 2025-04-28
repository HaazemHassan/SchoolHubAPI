using MediatR;
using School.Core.Features.Students.Queries.Results;
using School.Core.Wrappers;
using School.Data.Helpers;

namespace School.Core.Features.Students.Queries.Models
{
    public class GetStudentsPaginatedQuery : IRequest<PaginatedResult<GetStudentsPaginatedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
        public StudentOrderingEnum? OrderBy { get; set; }
    }
}
