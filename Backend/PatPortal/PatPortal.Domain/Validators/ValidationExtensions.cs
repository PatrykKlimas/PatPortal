using FluentValidation;
using FluentValidation.Results;
using PatPortal.SharedKernel.Enums;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Domain.Validators
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, byte[]> HasValidImage<T>(this IRuleBuilder<T, byte[]> ruleBuilder)
        {
            return (IRuleBuilderOptions<T, byte[]>)ruleBuilder.Custom((photo, context) =>
            {
                if (photo != null && photo.Length > 0)
                {
                    if (photo.GetImageFormat() == ImageFormat.Unknown)
                        context.AddFailure(new ValidationFailure("Photo", "Invalid image format."));
                }
            });
        }
    }
}
