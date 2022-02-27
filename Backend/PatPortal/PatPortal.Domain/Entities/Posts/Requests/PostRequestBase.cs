namespace PatPortal.Domain.Entities.Posts.Requests
{
    public class PostRequestBase
    {
        public byte[] Photo { get; private set; }
        public string Access { get; private set; }
        public Guid OwnerId { get; private set; }
        public string Content { get; private set; }
        public PostRequestBase(byte[] photo, string access, Guid ownerId, string content)
        {
            Photo = photo;
            Access = access;
            OwnerId = ownerId;
            Content = content;
        }

    }
}
