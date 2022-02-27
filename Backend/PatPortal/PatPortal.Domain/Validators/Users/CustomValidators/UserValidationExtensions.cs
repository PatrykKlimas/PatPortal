using FluentValidation;
using FluentValidation.Results;
using PatPortal.SharedKernel.Enums;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Domain.Validators.Users.CustomValidators
{
    public static class UserValidationExtensions
    {
        public static IRuleBuilderOptions<T, DateTime> HasValidDayOfBirth<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
        {
            return (IRuleBuilderOptions<T, DateTime>)ruleBuilder.Custom((date, context) =>
             {
                 var years = DateTime.Now.Year - date.Year;
                 if (years > 120 || years <= 0)
                     context.AddFailure(new ValidationFailure("Date of Birth", "Invalid date provided."));
             });
        }
    }
}
