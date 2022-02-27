using FluentValidation;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Validators.Users.CustomValidators;

namespace PatPortal.Domain.Validators.Users
{
    public class BaseUserValidator<TUser> : AbstractValidator<TUser> where TUser : UserBase
    {
        public BaseUserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotNull()
                .NotEmpty().Length(2, 40)
                .WithMessage("Name should be provided with at least 2 and no more than 40 characters.");

            RuleFor(user => user.LastName)
                .NotNull()
                .NotEmpty().Length(2,40)
                .WithMessage("Last Name should be provided with at least 2 and no more than 40 characters.");

            RuleFor(user => user.Email.ToString())
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid e-mail address.");

            RuleFor(user => user.DayOfBirht)
                .NotNull()
                .NotEmpty()
                .HasValidDayOfBirth();
        }
    }
}
