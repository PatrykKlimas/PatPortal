using PatPortal.Identity.Domain.Entities.Request;
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

            var password = userLogin.Password.Hashe();

            Autenticate(password , user.Password);
            var token = _identityProvider.GenerateToken(user);

            throw new NotImplementedException();
        }

        private void Autenticate(string givenPassword, string currentPassword)
        {
            if (!givenPassword.Equals(currentPassword))
                throw new UnauthorizedAccessException();//ToDo throw custom exception
        }
    }
}
