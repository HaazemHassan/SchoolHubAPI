﻿using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Students.Commands.Models
{
    public class UpdateStudentCommand : IRequest<Response<string>>
    {
        public int StudID { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public int? DepartmentId { get; set; }
        public string? Phone { get; set; }
    }
}
