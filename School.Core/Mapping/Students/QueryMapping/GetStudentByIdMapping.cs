using School.Core.Features.Students.Queries.Results;
using School.Data.Entities;

namespace School.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void GetStudentByIdMapping()
        {
            CreateMap<Student, GetStudentByIdResponse>()
                      .ForMember(dest => dest.DepartmentName
                      , opt => opt.MapFrom(src => src.Department.DName));
        }
    }
}
