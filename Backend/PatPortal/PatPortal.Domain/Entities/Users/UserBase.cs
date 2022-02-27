using PatPortal.Domain.Common;
using PatPortal.Domain.ValueObjects;

namespace PatPortal.Domain.Entities.Users
{
    public abstract class UserBase : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Email Email { get; set; }
        public DateTime DayOfBirht { get; set; }
        protected UserBase(
            Guid id,
            string firstName, 
            string lastName, 
            Email email, 
            DateTime dayOfBirht)
            : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DayOfBirht = dayOfBirht;
        }
    }
}
