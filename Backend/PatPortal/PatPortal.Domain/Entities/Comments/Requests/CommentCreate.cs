namespace PatPortal.Domain.Entities.Comments.Requests
{
    public class CommentCreate : CommentRequestBase
    {
        public CommentCreate(
            Guid ownerid, 
            string content,
            Guid postId) : 
            base(ownerid, content, postId)
        {
        }
    }
}
