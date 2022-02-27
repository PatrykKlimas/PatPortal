using MediatR;
using PatPortal.Application.DTOs.Request.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Application.Contracts.Commands.Posts
{
    public class UpdatePostsCommand : IRequest
    {
        public PostForUpdateDto Post { get; }
        public UpdatePostsCommand(PostForUpdateDto post)
        {
            Post = post;
        }

    }
}
