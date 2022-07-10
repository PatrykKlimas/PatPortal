using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Application.DTOs.Request.Comments
{
    public class CommentForCreationDto
    {
        public string Ownerid { get; set; }
        public string Content { get; set; }
    }
}
