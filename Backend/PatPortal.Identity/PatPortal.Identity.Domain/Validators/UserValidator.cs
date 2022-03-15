using FluentValidation;
using PatPortal.Identity.Domain.Entities;

namespace PatPortal.Identity.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserName)
                .NotNull()
                .NotEmpty().Length(4, 40)
                .WithMessage("Invalid user name");

            RuleFor(user => user.Password)
                .NotEmpty()
                .NotNull()
                .WithMessage("Password cannot be null")
                .Length(6, 20)
                .WithMessage("Password must be between 6 and 20 characters.")
                .Matches("[0-9]")
                .WithMessage("Password must contain at least one number")
                .Matches("[a-z]")
                .WithMessage("Password must contain at least one lower letter")
                .Matches("[A-Z]")
                .WithMessage("Password must contain at least one upper letter")
                .Matches(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]")
                .WithMessage("Password must contain at least one special character");

            RuleFor(user => user.Email.ToString())
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid e-mail address.");

            RuleFor(user => user.FirstName)
                .NotNull()
                .NotEmpty().Length(2, 40)
                .WithMessage("Name should be provided with at least 2 and no more than 40 characters.");

            RuleFor(user => user.LastName)
                .NotNull()
                .NotEmpty().Length(2, 40)
                .WithMessage("Last Name should be provided with at least 2 and no more than 40 characters.");
        }
    }
}
