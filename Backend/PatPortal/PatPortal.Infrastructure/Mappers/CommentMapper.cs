using PatPortal.Infrastructure.Factories.Interfaces;
using PatPortal.Infrastructure.Mappers.Interfaces;
using CommentDb = PatPortal.Database.Models.Comment;
using CommentEntity = PatPortal.Domain.Entities.Comments.Comment;

namespace PatPortal.Infrastructure.Mappers
{
    public class CommentMapper : ICommentMapper
    {
        private readonly IPostMapper _postMapper;
        private readonly IUserMapper _userMapper;

        public CommentMapper(IPostMapper postMapper, IUserMapper userMapper)
        {
            _postMapper = postMapper;
            _userMapper = userMapper;
        }
        public CommentDb Create(CommentEntity comment)
        {
            var post = _postMapper.Create(comment.Post);
            var user = _userMapper.Create(comment.Owner);

            return new CommentDb()
            {
                Owner = user,
                OwnerId = user.Id,
                Content = comment.Content,
                AddedDate = comment.AddedDate,
                EditedTime =comment.EditedTime,
                IsDeleted = false,
                Post = post,
                PostId = post.Id
            };
        }

        public CommentEntity Create(CommentDb comment)
        {
            var user = _userMapper.Create(comment.Owner);
            var post = _postMapper.Create(comment.Post);

            return new CommentEntity(
                comment.Id, 
                user, 
                comment.Content,
                comment.AddedDate, 
                comment.EditedTime, 
                post);
        }
    }
}
