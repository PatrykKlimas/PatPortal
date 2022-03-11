namespace PatPortal.Identity.Domain.Exceptions
{
    public class InitValidationException : Exception
    {
        public InitValidationException()
        {
        }
        public InitValidationException(string message)
            : base(message)
        {
        }
        public InitValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
