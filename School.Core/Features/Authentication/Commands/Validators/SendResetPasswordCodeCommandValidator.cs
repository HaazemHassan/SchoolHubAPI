using FluentValidation;
using School.Core.Features.Authentication.Commands.Models;

namespace School.Core.Features.Authentication.Commands.Validators
{
    internal class SendResetPasswordCodeCommandValidator : AbstractValidator<SendResetPasswordCodeCommand>
    {
        public SendResetPasswordCodeCommandValidator()
        {
            ApplyValidaionRules();
        }
        public void ApplyValidaionRules()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .NotNull().WithMessage(errorMessage: "{PropertyName} is required")
                .EmailAddress();

        }

    }
}
