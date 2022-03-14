using PatPortal.Identity.Domain.Common;
using PatPortal.Identity.Domain.Enums;
using PatPortal.Identity.Domain.ValueObjects;

namespace PatPortal.Identity.Domain.Entities
{
    public class User : Entity
    {
        public User(
            Guid id, 
            string userName, 
            string password, 
            Email email, 
            Role role, 
            string firstName, 
            string lastName, 
            Guid globalId)
            : base(id)
        {
            UserName = userName;
            Password = password;
            Email = email;
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            GlobalId = globalId;
        }
        public string UserName { get; }
        public string Password { get; }
        public Email Email { get; }
        public Role Role { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Guid GlobalId { get; }
    }
}
