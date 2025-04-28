using School.Core.Features.Students.Commands.Models;
using School.Data.Entities;

namespace School.Core.Mapping.Students
{
    public partial class StudentProfile
    {
        public void UpdateStuentMapping()
        {
            CreateMap<UpdateStudentCommand, Student>()
                  .ForMember(dest => dest.DID, opt => opt.MapFrom(src => src.DepartmentId));



        }
    }
}