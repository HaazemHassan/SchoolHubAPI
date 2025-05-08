using FluentValidation;
using School.Core.Features.Authorization.Commands.Models;

namespace School.Core.Features.Authorization.Commands.Validators
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleValidator()
        {
            ApplyValidaionRules();
        }
        public void ApplyValidaionRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .NotNull().WithMessage("{PropertyName} is required");

        }

    }
}