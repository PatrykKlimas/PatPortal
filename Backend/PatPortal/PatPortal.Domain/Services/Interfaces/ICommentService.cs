using PatPortal.Domain.Entities.Comments.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Domain.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Guid> CreateAsync(CommentCreate commentCreate);
        Task UpdateAsync(CommentUpdate commentUpdate);

    }
}
