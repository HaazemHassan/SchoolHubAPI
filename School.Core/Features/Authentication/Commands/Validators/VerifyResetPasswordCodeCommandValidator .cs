using FluentValidation;
using School.Core.Features.Authentication.Commands.Models;

namespace School.Core.Features.Authentication.Commands.Validators
{
    internal class VerifyResetPasswordCodeCommandValidator : AbstractValidator<VerifyResetPasswordCodeCommand>
    {
        public VerifyResetPasswordCodeCommandValidator()
        {
            ApplyValidaionRules();
        }
        public void ApplyValidaionRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .NotNull().WithMessage(errorMessage: "{PropertyName} is required")
                .EmailAddress();

            RuleFor(x => x.Code)
               .NotEmpty().WithMessage("{PropertyName} can't be empty")
               .NotNull().WithMessage(errorMessage: "{PropertyName} is required");

        }

    }
}
