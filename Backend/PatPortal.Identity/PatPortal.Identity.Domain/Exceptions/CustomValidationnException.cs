using System.Runtime.Serialization;

namespace PatPortal.Identity.Domain.Exceptions
{
    public class CustomValidationnException : Exception
    {
        public CustomValidationnException()
        {
        }
        public CustomValidationnException(string? message) 
            : base(message)
        {
        }
        public CustomValidationnException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
