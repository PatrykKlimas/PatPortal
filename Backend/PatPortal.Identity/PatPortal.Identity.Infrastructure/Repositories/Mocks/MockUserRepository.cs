using PatPortal.Identity.Domain.Entities;
using PatPortal.Identity.Domain.Enums;
using PatPortal.Identity.Domain.Repositories;
using PatPortal.Identity.Domain.ValueObjects;

namespace PatPortal.Identity.Infrastructure.Repositories.Mocks
{
    public class MockUserRepository : IUserRepository
    {
        List<User> _users;

        public MockUserRepository()
        {
            _users = new List<User>(){
                new User(
                    id:             Guid.NewGuid(),
                    userName:       "Patryk12345",
                    email:          new Email("david@gmail.com"),
                    firstName:      "David",
                    lastName:       "Rolex",
                    password:       "H3sXtFKYnUQREERFtcuMmtQ0VtG67JLvEk9VvwOB4Oxq5+vlcn4iUhdTbE9slsTXSiydSBkQ/ktdd7BqkuRW9l8ZQCCcHA==", //"xxxyyyzzz"
                    role:           Role.User,
                    globalId:       Guid.Parse("afdb21f2-66ad-48a2-a927-83f4d72a5258")
            ),
                new User(
                    id:            Guid.NewGuid(),
                    userName:      "Sarah12345",
                    email:         new Email("sarah@gmail.com"),
                    firstName:     "Sarah",
                    lastName:      "Doxter",
                    password:      "H3sXtFKYnUQREERFtcuMmtQ0VtG67JLvEk9VvwOB4Oxq5+vlcn4iUhdTbE9slsTXSiydSBkQ/ktdd7BqkuRW9l8ZQCCcHA==", //"xxxyyyzzz"
                    role:          Role.User,
                    globalId:      Guid.Parse("c2debe6b-619d-4595-aadb-c8878718af59")
            ),
                new User(
                    id:            Guid.NewGuid(),
                    userName:      "Anna12345",
                    email:         new Email("anna@gmail.com"),
                    firstName:     "Anna",
                    lastName:      "Koment",
                    password:      "H3sXtFKYnUQREERFtcuMmtQ0VtG67JLvEk9VvwOB4Oxq5+vlcn4iUhdTbE9slsTXSiydSBkQ/ktdd7BqkuRW9l8ZQCCcHA==", //"xxxyyyzzz"
                    role:          Role.User,
                    globalId:      Guid.Parse("9538dbb3-2913-4630-b0c3-2fc656e16a05")
            ),
                new User(
                    id:            Guid.NewGuid(),
                    userName:      "Sana12345",
                    email:         new Email("sana@ggmail.com"),
                    firstName:     "Sana",
                    lastName:      "Kiliwoda",
                    password:      "H3sXtFKYnUQREERFtcuMmtQ0VtG67JLvEk9VvwOB4Oxq5+vlcn4iUhdTbE9slsTXSiydSBkQ/ktdd7BqkuRW9l8ZQCCcHA==", //"xxxyyyzzz"
                    role:          Role.User,
                    globalId:      Guid.Parse("8e62999b-35e7-457b-b9e8-cfac048d3e8d")
            ),
                new User(
                    id:            Guid.NewGuid(),
                    userName:      "Peter12345",
                    email:         new Email("pete@02.com"),
                    firstName:     "Peter",
                    lastName:      "Kiliwoda",
                    password:      "H3sXtFKYnUQREERFtcuMmtQ0VtG67JLvEk9VvwOB4Oxq5+vlcn4iUhdTbE9slsTXSiydSBkQ/ktdd7BqkuRW9l8ZQCCcHA==", //"xxxyyyzzz"
                    role:          Role.User,
                    globalId:      Guid.Parse("ca23fd70-7928-478c-8349-299f43ba54bc")
            ) };
        }

        public async Task<User> AddAsync(User user)
        {
            var users = _users;
            users.Add(user);

            _users = users;
            var createdUser = _users.FirstOrDefault(u => u.Id == user.Id);

            return await Task.FromResult(createdUser);
        }

        public async Task<User> GetByEmailOrDefaultsync(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email.ToString().ToLower().Equals(email.ToLower()));
            return await Task.FromResult(user);
        }

        public async Task<User> GetByGlobalIdOrDefaultsync(Guid globalId)
        {
            var user = _users.FirstOrDefault(u => u.GlobalId == globalId);
            return await Task.FromResult(user);
        }

        public async Task<User> GetByUserNameOrDefaultAsync(string userName)
        {
            var user = _users.FirstOrDefault(u => u.UserName.ToLower().Equals(userName.ToLower()));
            return await Task.FromResult(user);
        }
    }
}
