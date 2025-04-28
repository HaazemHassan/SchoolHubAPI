using School.Core.Features.Students.Queries.Results;
using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace School.Core.Mapping.Students.QueryMapping     //this is wrong (namespace of partial class must be the same in all files)
namespace School.Core.Mapping.Students                    
{
    public partial class StudentProfile                   //file name is different from class name (it's okay)
    {
        public void GetStudentsMapping()
        {
            CreateMap<Student, GetStudentsResponse>()
                      .ForMember(dest => dest.DepartmentName
                      , opt => opt.MapFrom(src => src.Department.DName));
        }
    }
}
