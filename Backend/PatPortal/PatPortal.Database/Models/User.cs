using PatPortal.Database.Models.Common;

namespace PatPortal.Database.Models
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Proffestion { get; set; }
        public DateTime DayOfBirth { get; set; }
        public byte[] Photos { get; set; } //ToDo - think how to save it
        public bool IsDeleted { get; set; } //Applying soft delete
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Friendship> Friendships { get; set; }
    }
}
