using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Domain.Entities.Posts.Requests
{
    public class PostUpdate : PostRequestBase
    {
        public Guid Id { get; set; }
        public PostUpdate(
            Guid id,
            byte[] photo,
            string access, 
            Guid ownerId, 
            string content) : 
            base(photo, access, ownerId, content)
        {
            Id = id;
        }
    }
}
