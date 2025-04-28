using MediatR;
using School.Core.Bases;
using System.ComponentModel.DataAnnotations;

namespace School.Core.Features.Students.Commands.Models
{
    public class AddStudentCommand : IRequest<Response<string>>

    {
        public string Name { get; set; }
        public string? Address { get; set; }
        public int? DepartmentId { get; set; }

        [Display(Name = "Phone number")]
        public string? Phone { get; set; }

    }
}
