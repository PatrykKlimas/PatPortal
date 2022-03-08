using PatPortal.Identity.Domain.Common;
using PatPortal.Identity.Domain.Enums;
using PatPortal.Identity.Domain.ValueObjects;

namespace PatPortal.Identity.Domain.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Email Email { get; set; }
        public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
