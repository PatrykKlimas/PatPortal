using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PatPortal.Identity.Domain.Entities.Request;
using PatPortal.Identity.Domain.Repositories;
using PatPortal.Identity.Domain.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System;

namespace PatPortal.Identity.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Login(UserLogin userLogin)
        {
            var userByNameTask = _userRepository.GetByUserNameOrDefaultAsync(userLogin.UserName);
            var userByEmailTask = _userRepository.GetByEmailOrDefaultsync(userLogin.UserName);

            await Task.WhenAll(userByNameTask, userByEmailTask);
            var user = userByNameTask.Result == default ? userByEmailTask.Result : userByNameTask.Result;

            if(user == default)
                throw new NotImplementedException();


            throw new NotImplementedException();
        }

        private void Autenticate(string givenPassword, string currentPassword)
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RSACryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: givenPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}
