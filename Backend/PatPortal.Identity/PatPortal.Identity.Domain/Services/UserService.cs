using FluentValidation;
using PatPortal.Identity.Domain.Entities;
using PatPortal.Identity.Domain.Entities.Request;
using PatPortal.Identity.Domain.Entities.Response;
using PatPortal.Identity.Domain.Enums;
using PatPortal.Identity.Domain.Exceptions;
using PatPortal.Identity.Domain.Repositories;
using PatPortal.Identity.Domain.Services.Interfaces;
using PatPortal.Identity.Domain.ValueObjects;
using System.Security.Claims;

namespace PatPortal.Identity.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityProvider _identityProvider;
        private readonly IValidator<User> _userValidator;

        public UserService(
            IUserRepository userRepository, 
            IIdentityProvider identityProvider,
            IValidator<User> userValidator)
        {
            _userRepository = userRepository;
            _identityProvider = identityProvider;
            _userValidator = userValidator;
        }

        public async Task<UserCredentials> Login(UserLogin userLogin)
        {
            var userByNameTask = _userRepository.GetByUserNameOrDefaultAsync(userLogin.UserName);
            var userByEmailTask = _userRepository.GetByEmailOrDefaultsync(userLogin.UserName);

            await Task.WhenAll(userByNameTask, userByEmailTask);
            var user = userByNameTask.Result == default ? userByEmailTask.Result : userByNameTask.Result;

            if (user == default)
                throw new EntityNotFoundException($"User with login: {userLogin.UserName} does not exist.");

            var autenticationResult = _identityProvider.Autenticate(userLogin.Password, user.Password);

            if(!autenticationResult)
                throw new UnauthorizedException("Invalid password.");

            var token = _identityProvider.GenerateToken(user);
            var credentials = new UserCredentials(user.GlobalId, token);

            return credentials;
        }

        public async Task<User> GetAsync(ClaimsIdentity claims)
        {
            var email = _identityProvider.GetEmailOrDefault(claims);

            if (email is null)
                throw new DomainValidationException("Invalid claims provided.");

            var user = await _userRepository.GetByEmailOrDefaultsync(email);

            if (user is null)
                throw new EntityNotFoundException("There is no user for provided claims.");

            return user;
        }

        public async Task<Guid> CreateAsync(UserCreate userCreate)
        {
            var user = new User(
                Guid.NewGuid(), 
                userCreate.UserName, 
                userCreate.Password, 
                new Email(userCreate.Email), 
                Role.User, 
                userCreate.FirstName, 
                userCreate.LastName, 
                userCreate.GlobalId);

            var validationResult = _userValidator.Validate(user);

            if (validationResult.IsValid is false)
                throw new CustomValidationnException(validationResult);

            var userByName = _userRepository.GetByUserNameOrDefaultAsync(user.UserName);
            var userByEmail = _userRepository.GetByEmailOrDefaultsync(user.Email.ToString());
            var userByGlobalId = _userRepository.GetByGlobalIdOrDefaultsync(user.GlobalId);

            await Task.WhenAll(userByEmail, userByGlobalId, userByName);

            if (userByEmail.Result is not null)
                throw new DomainValidationException($"User with email address: {user.Email} already exists.");
            if (userByName.Result is not null)
                throw new DomainValidationException($"User with user name: {user.UserName} already exists.");
            if (userByGlobalId.Result is not null)
                throw new DomainValidationException($"User with id: {user.Email} already exists.");

            var createdUser = await _userRepository.AddAsync(user);

            return createdUser.Id;
        }
    }
}
