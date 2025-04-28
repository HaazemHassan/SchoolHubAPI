using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Students.Queries.Models;
using School.Core.Features.Students.Queries.Results;
using School.Core.SharedResources;
using School.Core.Wrappers;
using School.Services.ServicesContracts;

namespace School.Core.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler,
                 IRequestHandler<GetStudentsQuery, Response<List<GetStudentsResponse>>>,
                 IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdResponse>>,
                 IRequestHandler<GetStudentsPaginatedQuery, PaginatedResult<GetStudentsPaginatedResponse>>


    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resources> _localizer;




        public StudentQueryHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<Resources> loalizer) : base(loalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _localizer = loalizer;
        }
        public async Task<Response<List<GetStudentsResponse>>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var studentList = await _studentService.GetStudentsAsync();
            var studentListReponse = _mapper.Map<List<GetStudentsResponse>>(studentList);
            return Success(studentListReponse);
        }

        public async Task<Response<GetStudentByIdResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            int id = request.Id;

            if (id < 1)
                return BadRequest<GetStudentByIdResponse>(_localizer[ResourcesKeys.InvalidId]);

            var student = await _studentService.GetStudentAsync(s => s.StudID == id);
            if (student is null)
                return NotFound<GetStudentByIdResponse>(_localizer[ResourcesKeys.NotFound, _localizer["Student"]]);

            var studentMapped = _mapper.Map<GetStudentByIdResponse>(student);
            return Success(studentMapped);
        }

        public async Task<PaginatedResult<GetStudentsPaginatedResponse>> Handle(GetStudentsPaginatedQuery request, CancellationToken cancellationToken)
        {

            var studentsQueryable = _studentService.GetQueryable(request.OrderBy, request.Search)
                .Include(x => x.Department)
                .Select(s => new GetStudentsPaginatedResponse(
                          s.StudID, s.Name, s.Address,
                          s.Department != null ? s.Department.DName : null)
                 );

            var paginatedList = await studentsQueryable.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
    }




}
