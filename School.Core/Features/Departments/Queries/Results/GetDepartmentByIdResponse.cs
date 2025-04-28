using School.Core.DTO;
using School.Core.Wrappers;

namespace School.Core.Features.Departments.Queries.Results
{
    public class GetDepartmentByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Supervisor { get; set; }

        public PaginatedResult<StudentBasicDTO> Students { get; set; }
        public List<InstructorBasicDTO> Instructors { get; set; } = new List<InstructorBasicDTO>();
        public List<SubjectBasicDTO> Subjects { get; set; } = new List<SubjectBasicDTO>();

    }


}
