using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using School.Core.Bases;
using School.Core.Features.Students.Commands.Models;
using School.Data.Entities;
using School.Services.Bases;
using School.Services.ServicesContracts;

namespace School.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>,
                                        IRequestHandler<UpdateStudentCommand, Response<string>>,
                                        IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources.Resources> _loalizer;


        public StudentCommandHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResources.Resources> loalizer) : base(loalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _loalizer = loalizer;
        }




        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapped = _mapper.Map<Student>(request);
            var result = await _studentService.AddAsync(studentMapped);

            if (result == ServiceOpertaionResult.DependencyNotExist)
                return BadRequest<string>("Department not exists");


            return Created(" Added successfully");

        }

        public async Task<Response<string>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var studentMapped = _mapper.Map<Student>(request);
            ServiceOpertaionResult result = await _studentService.UpdateAsync(studentMapped);

            if (result == ServiceOpertaionResult.NotExist)
                return NotFound<string>("Student does not exist");

            return Updated<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var result = await _studentService.DeleteAsync(request.Id);

            if (result == ServiceOpertaionResult.NotExist)
                return NotFound<string>("Student does not exist");

            return Deleted<string>();
        }
    }
}
