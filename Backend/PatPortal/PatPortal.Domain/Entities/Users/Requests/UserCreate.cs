using PatPortal.Domain.ValueObjects;

namespace PatPortal.Domain.Entities.Users
{
    public class UserCreate : UserBase
    {
        public UserCreate(
            Guid id, 
            string firstName, 
            string lastName, 
            Email email, 
            DateTime dayOfBirht) : 
            base(id, firstName, lastName, email, dayOfBirht)
        {
        }
    }
}
