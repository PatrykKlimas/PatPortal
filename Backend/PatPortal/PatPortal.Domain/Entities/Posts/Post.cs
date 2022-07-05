using PatPortal.Domain.Common;
using PatPortal.Domain.Entities.Comments;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Enums;

namespace PatPortal.Domain.Entities.Posts
{
    public class Post : Entity
    {
        public byte[] Photo { get; private set; }
        public string Content { get; private set; }
        public DataAccess Access { get; private set; }
        public User Owner { get; private set; }
        public string OwnerName  { get; private set; }
        public DateTime AddedDate { get; private set; }
        public DateTime EditedTime { get; private set; }
        public Post(Guid Id, 
            byte[] photo, 
            string content,
            DataAccess access, 
            User owner, 
            DateTime addedDate,
            DateTime editedTime) : base(Id)
        {
            Photo = photo;
            Content = content;
            Access = access;
            Owner = owner;
            OwnerName = Owner.FirstName + " " + Owner.LastName;
            AddedDate = addedDate;
            EditedTime = editedTime;
        }
    }
}
