using PatPortal.Database.Models.Common;

namespace PatPortal.Database.Models
{
    public class Friendship : Entity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public User Friend { get; set; }
        public Guid FriendId { get; set; }
        public bool IsDeleted { get; set; } //Applying soft delete
        public bool InviteAccepted { get; set; }
        public bool UserInvited { get; set; }
    }
}
