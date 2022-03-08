using PatPortal.Identity.Domain.Common;

namespace PatPortal.Identity.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string EmailAddress { get; private set; }

        public Email(string emailAddress)
        {
            this.EmailAddress = emailAddress;
        }
        public override string ToString()
        {
            return EmailAddress;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EmailAddress;
        }
    }
}
