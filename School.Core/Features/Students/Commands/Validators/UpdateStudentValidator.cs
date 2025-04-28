using FluentValidation;
using School.Core.Features.Students.Commands.Models;
using School.Services.ServicesContracts;

namespace School.Core.Features.Students.Commands.Validators
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
    {
        private readonly IStudentService _studentService;
        public UpdateStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;

            ApplyValidaionRules();
            ApplyCustomValidaionRules();
        }


        public void ApplyValidaionRules()
        {
            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(20);

            RuleFor(x => x.Address)
                .MinimumLength(3)
                .MaximumLength(20);


            RuleFor(x => x.DepartmentId)
                .GreaterThan(0);

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number is not valid.");
        }

        public void ApplyCustomValidaionRules()
        {

            RuleFor(x => x.Name)
                .MustAsync(async (Model, Key, CancellationToken)
                   => !await _studentService.IsNameExistsExcludeSelf(Key, Model.StudID))
                .WithMessage("{PropertyName} already exists.");


        }
    }
}
