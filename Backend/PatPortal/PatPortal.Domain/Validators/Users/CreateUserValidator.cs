using PatPortal.Domain.Entities.Users;

namespace PatPortal.Domain.Validators.Users
{
    public class CreateUserValidator : BaseUserValidator<UserCreate>
    {
        public CreateUserValidator() : base()
        {
        }
    }
}
