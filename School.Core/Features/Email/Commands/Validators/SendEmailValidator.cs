using FluentValidation;
using School.Core.Features.Email.Commands.Models;

namespace School.Core.Features.Email.Commands.Validators
{
    public class SendEmailValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailValidator()
        {
            ApplyValidaionRules();
        }


        public void ApplyValidaionRules()
        {
            RuleFor(x => x.ReceiverEmail)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .NotNull().WithMessage("{PropertyName} can't be null")
                .EmailAddress().WithMessage("{PropertyName} must be a valid email address.");


            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .NotNull().WithMessage("{PropertyName} can't be null");

        }


    }
}
