using FluentValidation;
using School.Core.Features.User.Commands.Models;

namespace School.Core.Features.User.Commands.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            ApplyValidaionRules();
        }

        public void ApplyValidaionRules()
        {
            RuleFor(x => x.FullName)
                .MinimumLength(4)
                .MaximumLength(20)
                .Matches(@"^[\p{L}\s]+$").WithMessage(errorMessage: "Full name must contain only letters and spaces.");


            RuleFor(x => x.UserName)
                .MinimumLength(4)
                .MaximumLength(20)
                .Matches(@"^[a-zA-Z0-9._]+$").WithMessage("{PropertyName} can only contain letters, numbers, dots, and underscores.");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .NotNull().WithMessage("{PropertyName} can't be null");


            RuleFor(x => x.PhoneNumber)
                .Matches(expression: @"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number is not valid.");
        }
    }
}

