using PatPortal.Identity.Domain.Entities.Request;
using PatPortal.Identity.Domain.Exceptions;
using PatPortal.Identity.Domain.Repositories;
using PatPortal.Identity.Domain.Services.Interfaces;
using PatPortal.Identity.SharedKernel;

namespace PatPortal.Identity.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityProvider _identityProvider;

        public LoginService(IUserRepository userRepository, IIdentityProvider identityProvider)
        {
            _userRepository = userRepository;
            _identityProvider = identityProvider;
        }

        public async Task<string> Login(UserLogin userLogin)
        {
            var userByNameTask = _userRepository.GetByUserNameOrDefaultAsync(userLogin.UserName);
            var userByEmailTask = _userRepository.GetByEmailOrDefaultsync(userLogin.UserName);

            await Task.WhenAll(userByNameTask, userByEmailTask);
            var user = userByNameTask.Result == default ? userByEmailTask.Result : userByNameTask.Result;

            if(user == default)
                throw new NotImplementedException();

            var autenticationResult = _identityProvider.Autenticate(userLogin.Password, user.Password);

            if(!autenticationResult)
                throw new UnauthorizedException("Invalid password.");

            var token = _identityProvider.GenerateToken(user);

            return token;
        }

        private void Autenticate(string givenPassword, string currentPassword)
        {
            if (!givenPassword.Equals(currentPassword))
                throw new UnauthorizedException("Invalid password.");
        }
    }
}
