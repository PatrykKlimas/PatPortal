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
                .WithMessage("Password must be between 6 and 20 characters.");
                //TODO - check password
                //.Matches("/^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9]).*$/")
                //.WithMessage("Must have atleast 1 uppercase, 1 lowercase letter and 1 number");

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
