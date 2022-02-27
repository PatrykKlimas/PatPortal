namespace PatPortal.Domain.Exceptions
{
    public class DomainValidationException : Exception
    {
        public DomainValidationException()
        {
        }

        public DomainValidationException(string message)
            : base(message)
        {
        }

        public DomainValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
