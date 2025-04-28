using School.Core.DTO;
using School.Core.Features.Departments.Queries.Results;
using School.Data.Entities;

namespace School.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentDetailedByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdResponse>()
                      .ForMember(dest => dest.Id,
                             opt => opt.MapFrom(src => src.DID))
                      .ForMember(dest => dest.Name,
                             opt => opt.MapFrom(src => src.DName))
                      .ForMember(dest => dest.Supervisor,
                             opt => opt.MapFrom(src => src.Supervisor != null ? src.Supervisor.Name : null))
                      .ForMember(dest => dest.Students,
                             opt => opt.Ignore())
                      .ForMember(dest => dest.Subjects,
                             opt => opt.MapFrom(src => src.DepartmentSubjects))
                      .ForMember(dest => dest.Instructors,
                             opt => opt.MapFrom(src => src.Instructors));



            CreateMap<Instructor, InstructorBasicDTO>()
                      .ForMember(dest => dest.Id,
                             opt => opt.MapFrom(src => src.InsID))
                      .ForMember(dest => dest.Name,
                             opt => opt.MapFrom(src => src.Name));

            CreateMap<DepartmentSubject, SubjectBasicDTO>()
                .ForMember(dest => dest.Id,
                       opt => opt.MapFrom(src => src.SubID))
                .ForMember(dest => dest.Name,
                       opt => opt.MapFrom(src => src.Subject != null ? src.Subject.SubjectName : null));



        }
    }
}