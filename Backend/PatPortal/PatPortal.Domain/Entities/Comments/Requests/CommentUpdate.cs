using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Domain.Entities.Comments.Requests
{
    public class CommentUpdate : CommentRequestBase
    {
        public Guid Id { get; set; }
        public CommentUpdate(
            Guid id ,
            Guid ownerid, 
            string content, 
            Guid postId) : 
            base(ownerid, content, postId)
        {
            Id = id;
        }

    }
}
