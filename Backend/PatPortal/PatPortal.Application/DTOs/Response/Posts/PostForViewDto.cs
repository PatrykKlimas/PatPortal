using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Application.DTOs.Response.Posts
{
    public class PostForViewDto
    {
        public byte[] Photo { get; private set; }
        public string Content { get; private set; }
        public string Access { get; private set; }
        public string OwnerId { get; private set; }
        public string AddedDate { get; private set; }
        public string EditedTime { get; private set; }
    }
}
