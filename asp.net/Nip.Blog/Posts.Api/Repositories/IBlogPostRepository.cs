using System;
using System.Collections.Generic;
using System.Linq;
using Posts.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posts.Api.Repositories
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> GetAsync(long id);
        IAsyncEnumerable<BlogPost> GetAllAsync();
        Task AddAsync(BlogPost post);
        Task UpdateAsync(BlogPost post);
        Task DeleteAsync(long id);
    }
}
