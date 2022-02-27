namespace PatPortal.Application.DTOs.Request.Posts
{
    public class PostForUpdateDto
    {
        public string Id { get; set; }
        public byte[] Photo { get; set; }
        public string Access { get; set; }
        public string OwnerId { get; set; }
        public string Content { get; set; }
    }
}
