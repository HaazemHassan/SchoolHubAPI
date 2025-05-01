using FluentValidation;
using School.Core.Features.User.Commands.Models;

namespace School.Core.Features.User.Commands.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator()
        {
            ApplyValidationRules();
            ApplyCustomValidations();
        }

        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .NotNull().WithMessage("{PropertyName} can't be null");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("{PropertyName} can't be empty")
                .NotNull().WithMessage("{PropertyName} can't be null");

            When(x => x.FullName != null, () =>
            {
                RuleFor(x => x.FullName)
                    .MinimumLength(4).WithMessage("Full name must be at least 4 characters")
                    .MaximumLength(20).WithMessage("Full name must not exceed 20 characters")
                    .Matches(@"^[\p{L}\s]+$").WithMessage("Full name must contain only letters and spaces.");
            });

            When(x => x.UserName != null, () =>
            {
                RuleFor(x => x.UserName)
                    .MinimumLength(4).WithMessage("Username must be at least 4 characters")
                    .MaximumLength(20).WithMessage("Username must not exceed 20 characters")
                    .Matches(@"^[a-zA-Z0-9._]+$").WithMessage("Username can only contain letters, numbers, dots, and underscores.");
            });

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address)
                    .MinimumLength(4).WithMessage("{PropertyName} must be at least 4 characters")
                    .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters");
            });

            When(x => x.Country != null, () =>
            {
                RuleFor(x => x.Country)
                    .MinimumLength(4).WithMessage("{PropertyName} must be at least 4 characters")
                    .MaximumLength(20).WithMessage("{PropertyName} must not exceed 20 characters");
            });

            When(x => x.PhoneNumber != null, () =>
            {
                RuleFor(x => x.PhoneNumber)
                    .Matches(expression: @"^\+?[1-9]\d{1,14}$")
                    .WithMessage("Phone number is not valid.");
            });
        }

        public void ApplyCustomValidations()
        {
            RuleFor(x => x)
               .Must(HaveAtLeastOneNonNullProperty)
               .WithMessage("Nothing to change. At least one property must be provided for update.");
        }

        private bool HaveAtLeastOneNonNullProperty(UpdateUserCommand command)
        {
            return command.FullName != null ||
                   command.UserName != null ||
                   command.Address != null ||
                   command.Country != null ||
                   command.PhoneNumber != null;
        }
    }
}