using PatPortal.Domain.Common;
using PatPortal.Domain.Entities.Users;

namespace PatPortal.Domain.Entities.Friendships
{
    public class Friendship : Entity
    {
        public User User { get; private set; }
        public User Friend { get; private set; }
        public bool InviteAccepted { get; private set; }
        public bool UserInvited { get; private set; }
        public Friendship(Guid id, User user, User friend, bool inviteAccepted, bool userInvited) : base(id)
        {
            User = user;
            Friend = friend;
            InviteAccepted = inviteAccepted;
            UserInvited = userInvited;
        }

        public void AcceptInvitation()
        {
            InviteAccepted = true;
        }
    }
}
