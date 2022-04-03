using PatPortal.Database.Models.Common;

namespace PatPortal.Database.Models
{
    public class Comment : Entity
    {
        public User Owner { get; set; }
        public Guid OwnerId { get; set; }
        public string Content { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime EditedTime { get; set; }
        public bool IsDeleted { get; set; } //Applying soft delete
        public Post Post { get; set; }
        public Guid PostId { get; set; }
    }
}
