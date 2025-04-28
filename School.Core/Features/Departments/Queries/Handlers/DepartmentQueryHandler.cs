using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.DTO;
using School.Core.Features.Departments.Queries.Models;
using School.Core.Features.Departments.Queries.Results;
using School.Core.SharedResources;
using School.Core.Wrappers;
using School.Data.Entities;
using School.Services.ServicesContracts;

namespace School.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler, IRequestHandler<GetDepartmentDetailedByIdQuery, Response<GetDepartmentByIdResponse>>
    {
        private readonly IStringLocalizer<Resources> _localizer;
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public DepartmentQueryHandler(IStringLocalizer<Resources> localizer
                                    , IDepartmentService departmentService
                                    , IMapper mapper,
                                    IStudentService studentService) : base(localizer)
        {

            _localizer = localizer;
            _departmentService = departmentService;
            _mapper = mapper;
            _studentService = studentService;
        }

        public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentDetailedByIdQuery request, CancellationToken cancellationToken)
        {
            //doesn't include student (we want student list to be paginated )
            Department? department = await _departmentService.GetDepartmentDetailedByIdAsync(request.Id);
            if (department is null)
                return NotFound<GetDepartmentByIdResponse>();

            var DepartmentMapped = _mapper.Map<GetDepartmentByIdResponse>(department);


            var studentsPaginated = await _studentService.GetQueryable(s => s.DID == department.DID)
                .Select(s => new StudentBasicDTO(s.StudID, s.Name))
                .ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);

            DepartmentMapped.Students = studentsPaginated;

            return Success(DepartmentMapped);


        }
    }
}
