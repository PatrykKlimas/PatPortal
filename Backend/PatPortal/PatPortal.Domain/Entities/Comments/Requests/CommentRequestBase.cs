using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Domain.Entities.Comments.Requests
{
    public abstract class CommentRequestBase
    {
        public Guid Ownerid { get; private set; }
        public string Content { get; private set; }
        public Guid PostId { get; private set; }
        protected CommentRequestBase(Guid ownerid, string content, Guid postId)
        {
            Ownerid = ownerid;
            Content = content;
            PostId = postId;
        }

    }
}
