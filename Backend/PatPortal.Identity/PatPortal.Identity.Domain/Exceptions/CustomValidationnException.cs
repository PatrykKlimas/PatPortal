using FluentValidation.Results;

namespace PatPortal.Identity.Domain.Exceptions
{
    public class CustomValidationnException : Exception
    {
        public string errorMessage { get; set; } = string.Empty;
        public CustomValidationnException(ValidationResult validationResult)
        {
            string errorMessage = string.Empty;
            validationResult.Errors.ForEach(error => errorMessage += error.ErrorMessage + "\n");
            throw new CustomValidationnException(errorMessage);
        }
        public CustomValidationnException()
        {
        }
        public CustomValidationnException(string? message) 
            : base(message)
        {
        }
    }
}
