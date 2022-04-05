using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Entities.Users.Requests;
using PatPortal.Domain.Exceptions;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.Services.Interfaces;
using PatPortal.Domain.Validators.Factories.Interfaces;

namespace PatPortal.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserValidatorFactory _userValidatorFactory;
        private readonly IFriendshipRepository _friendshipRepository;

        public UserService(
            IUserRepository userRepository,
            IUserValidatorFactory userValidatorFactory,
            IFriendshipRepository friendshipRepository)
        {
            _friendshipRepository = friendshipRepository;
            _userRepository = userRepository;
            _userValidatorFactory = userValidatorFactory;
        }

        public async Task<Guid> CreateAsync(UserCreate userForCreate)
        {
            var userTask = _userRepository.GetOrDefaultByEmailAsync(userForCreate.Email);
            var validationResult = await _userValidatorFactory.Validate(userForCreate);

            if (!validationResult.IsValid)
                throw new CustomValidationnException(validationResult);

            if ((await userTask) is not null)
                throw new DomainValidationException($"User with email: {userForCreate.Email} already exists.");

            var newUser = new User(
                id: userForCreate.Id,
                firstName: userForCreate.FirstName,
                lastName: userForCreate.LastName,
                email: userForCreate.Email,
                profession: "",
                dayOfBirht: userForCreate.DayOfBirht,
                photo: new byte[] { });

            var user = await _userRepository.AddAsync(newUser);

            return user.Id;
        }

        public async Task UpdateAsync(UserUpdate userForUpdate)
        {
            var userByEmailTask = _userRepository.GetOrDefaultByEmailAsync(userForUpdate.Email);
            var userTask = _userRepository.GetOrDefaultAsync(userForUpdate.Id);

            var validationResult = await _userValidatorFactory.Validate(userForUpdate);

            if (!validationResult.IsValid)
                throw new CustomValidationnException(validationResult);

            var user = await userTask;
            if (user == default)
                throw new EntityNotFoundException($"User with id: {userForUpdate.Id} does not exist.");

            var userByEmail = await userByEmailTask;
            if (userByEmail != default && !userForUpdate.Email.Equals(user.Email))
            {
                if (userByEmail.Id != userForUpdate.Id)
                    throw new DomainValidationException($"User with email: {userForUpdate.Email} already exists.");
            }

            user.UpdateEmail(userForUpdate.Email);
            user.UpdateDayOfBirht(userForUpdate.DayOfBirht);
            user.UpdatePhoto(userForUpdate.Photo);
            user.UpdateLastName(userForUpdate.FirstName);
            user.UpdateProfesion(userForUpdate.Profession);

            await _userRepository.UpdateAsync(user);
        }

        public async Task<User> GetAsync(Guid userId)
        {
            var user = await GetUserOrThrow(userId);
            return user;
        }

        public async Task<IEnumerable<User>> GetFriendsAsync(Guid userId)
        {
            var user = await GetUserOrThrow(userId);
            var friendships = await _friendshipRepository.GetByUserAsync(userId, true);
            var users = friendships.Select(frs => frs.Friend);

            return users;
        }

        public async Task<IEnumerable<User>> GetInvitationsAsync(Guid userId)
        {
            var user = await GetUserOrThrow(userId);
            var friendships = await _friendshipRepository.GetByUserAsync(userId, false, false);
            var users = friendships.Select(frs => frs.Friend);

            return users;
        }

        public async Task<IEnumerable<User>> GetSendInvitationsAsync(Guid userId)
        {
            var user = await GetUserOrThrow(userId);
            var friendships = await _friendshipRepository.GetByUserAsync(userId, false, true);
            var users = friendships.Select(frs => frs.Friend);

            return users;
        }

        private async Task<User> GetUserOrThrow(Guid userId)
        {
            var user = await _userRepository.GetOrDefaultAsync(userId);

            if (user == default)
                throw new EntityNotFoundException($"User with id: {userId} not found.");

            return user;
        }
    }
}
