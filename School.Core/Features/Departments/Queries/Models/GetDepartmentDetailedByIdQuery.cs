using MediatR;
using School.Core.Bases;
using School.Core.Features.Departments.Queries.Results;

namespace School.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentDetailedByIdQuery : IRequest<Response<GetDepartmentByIdResponse>>
    {
        public int Id { get; set; }
        public int StudentPageNumber { get; set; }
        public int StudentPageSize { get; set; }
        public GetDepartmentDetailedByIdQuery(int id, int pageNumber = 1, int pageSize = 10)
        {
            Id = id;
            StudentPageNumber = pageNumber;
            StudentPageSize = pageSize;
        }
    }
}
