using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Application.DTOs.Response.Comments
{
    public class CommentForViewDto
    {
        public string Id { get; set; }
        public string OwnerId { get; private set; }
        public string OwnerName { get; private set; }
        public string Content { get; private set; }
        public string AddedDate { get; private set; }
        public string EditedTime { get; private set; }
        public string PostId { get; private set; }
    }
}
