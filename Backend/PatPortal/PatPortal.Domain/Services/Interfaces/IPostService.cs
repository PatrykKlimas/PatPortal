using PatPortal.Domain.Entities.Posts;
using PatPortal.Domain.Entities.Posts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Domain.Services.Interfaces
{
    public interface IPostService
    {
        Task<Guid> CreateAsync(PostCreate postCreate);
        Task UpdateAsync(PostUpdate postCreate);
        Task<IEnumerable<Post>> GetByUserAsync(Guid userId, Guid requestorId);
        Task<IEnumerable<Post>> GetForUserToSeeAsync(Guid userId);
    }
}
