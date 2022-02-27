using PatPortal.Domain.Entities.Users.Requests;
using FluentValidation;
using PatPortal.Domain.Validators.Users.CustomValidators;

namespace PatPortal.Domain.Validators.Users
{
    public class UpdateUserValidator : BaseUserValidator<UserUpdate>
    {
        public UpdateUserValidator() : base()
        {
            RuleFor(user => user.Profession)
                .Length(5,40)
                .When(uesr => uesr.Profession != null)
                .WithMessage("Profession should be provided with at least 5 and no more than 30 characters.");

            RuleFor(user => user.Photo)
                .HasValidImage();

        }
    }
}
