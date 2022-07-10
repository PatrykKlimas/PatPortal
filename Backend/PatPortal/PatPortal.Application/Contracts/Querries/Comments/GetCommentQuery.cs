using MediatR;
using PatPortal.Application.DTOs.Response.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Application.Contracts.Querries.Comments
{
    public class GetCommentQuery : IRequest<CommentForViewDto>
    {
        public GetCommentQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
