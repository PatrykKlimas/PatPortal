using PatPortal.Domain.Common;
using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Entities.Users;

namespace PatPortal.Domain.Entities.Comments
{
    public class Comment : Entity
    {
        public User Owner { get; private set; }
        public string OwnerName { get; private set; }
        public string Content { get; private set; }
        public DateTime AddedDate { get; private set; }
        public DateTime EditedTime { get; private set; }
        public Post Post { get; private set; }
        public Comment(
            Guid Id,
            User owner, 
            string content, 
            DateTime addedDate, 
            DateTime editedTime,
            Post post) : base(Id)
        {
            Owner = owner;
            OwnerName = owner.FirstName + " " + owner.LastName;
            Content = content;
            AddedDate = addedDate;
            EditedTime = editedTime;
            Post = post;
        }

        public void UpdateContent(string content)
        {
            Content = content;
        }

        public bool Equals(Comment comment)
        {
            if(comment == null)
                return false;

            return comment.Id.Equals(Id) && 
                   comment.Owner.Id.Equals(Owner.Id) && 
                   comment.Content.Equals(Content) && 
                   comment.Post.Id.Equals(Post.Id);
        }
    }
}
