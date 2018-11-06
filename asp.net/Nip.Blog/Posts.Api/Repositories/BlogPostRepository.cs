using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Posts.Api.Data;
using Posts.Api.Models;

namespace Posts.Api.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogPostContext _postsDbContext;
        public BlogPostRepository(BlogPostContext postsDbContext)
        {
            _postsDbContext = postsDbContext;
        }

        public async Task AddAsync(BlogPost post)
        {
            await _postsDbContext.BlogPosts.AddAsync(post);
            await _postsDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            BlogPost post = await GetAsync(id);
            if(post == null)
            {
                return;
            }
            _postsDbContext.BlogPosts.Remove(post);
            await _postsDbContext.SaveChangesAsync();
        }

        public IAsyncEnumerable<BlogPost> GetAllAsync()
        {
            return _postsDbContext.BlogPosts.ToAsyncEnumerable();
        }

        public async Task<BlogPost> GetAsync(long id)
        {
            return await _postsDbContext.BlogPosts.FindAsync(id);
        }

        public async Task UpdateAsync(BlogPost post)
        {
            _postsDbContext.BlogPosts.Update(post);
            await _postsDbContext.SaveChangesAsync();
        }
    }
}
