using PatPortal.Database.Models.Common;
using PatPortal.Domain.Enums;

namespace PatPortal.Database.Models
{
    public class Post : Entity
    {
        public byte[] Photo { get; set; }
        public string Content { get; set; }
        public DataAccess Access { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime EditedTime { get; set; }
        public User Owner { get; set; }
        public Guid OwnerId { get; set; }
        public bool IsDeleted { get; set; } //Applying soft delete
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
