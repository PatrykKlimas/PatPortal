using NSubstitute;
using NUnit.Framework;
using PatPortal.Domain.Repositories.Interfaces;
using PatPortal.Domain.Services;
using PatPortal.Domain.Services.Interfaces;

namespace PatPortal.Unit.Tests.Domain.Services
{
    public class FriendshipServiceTests
    {
        private IFriendshipService _friendshipService;
        private IFriendshipRepository _friendshipRepository;
        [SetUp]
        public void SetUp()
        {
            _friendshipRepository = Substitute.For<IFriendshipRepository>();
            _friendshipService = new FriendshipService(_friendshipRepository);
        }
    }
}
