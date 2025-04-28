using FluentValidation;
using School.Core.Features.Students.Commands.Models;
using School.Services.ServicesContracts;

namespace School.Core.Features.Students.Commands.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentCommand>
    {
        private readonly IStudentService _studentService;
        public AddStudentValidator(IStudentService studentService)
        {
            _studentService = studentService;


            ApplyValidaionRules();
            ApplyCustomValidaionRules();
        }


        // Localize this file later
        public void ApplyValidaionRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .NotNull().WithMessage("{PropertyName} can't be null")
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
            //***********************************//
            //this acts as remote validaion 
            RuleFor(x => x.Name)
                .MustAsync(async (Key, CancellationToken)
                   => !await _studentService.IsNameExistsAsync(Key))
                .WithMessage("{PropertyName} already exists.");

            //with data annotations we can use 
            //[Remote("nameof(IsNameExists)", "Student", ErrorMessage = "{0} already exists.")]

            //***********************************//

        }
    }
}
