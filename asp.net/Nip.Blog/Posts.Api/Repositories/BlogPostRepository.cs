using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<PaginatedItems<BlogPost>> GetAllPagedAsync(int pageIndex, int pageSize, Expression<Func<BlogPost, bool>> filter)
        {
            IQueryable<BlogPost> query = _postsDbContext.BlogPosts;
            if (filter != null)
                query = query.Where(filter);
            var totalItems = await query.CountAsync();
            var posts = query.OrderByDescending(c => c.Id).Skip(pageIndex * pageSize).Take(pageSize);

            var pagedPosts = new PaginatedItems<BlogPost>();
            pagedPosts.PageIndex = pageIndex;
            pagedPosts.PageSize = pageSize;
            pagedPosts.TotalItems = await _postsDbContext.BlogPosts.CountAsync();
            pagedPosts.Items = posts.AsQueryable();
            return pagedPosts;
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

        public async Task<IEnumerable<BlogPostComment>> GetCommentsAsync(long blogPostId)
        {
            var post = await _postsDbContext.BlogPosts.Include(x => x.Comments).Where(x => x.Id == blogPostId).FirstAsync();
            return post.Comments;
        }
        public async Task AddCommentAsync(long blogPostId, BlogPostComment comment)
        {
            BlogPost blogPost = await GetAsync(blogPostId);
            if (blogPost.Comments == null)
                blogPost.Comments = new List<BlogPostComment>();
            blogPost.Comments.Add(comment);
            await _postsDbContext.SaveChangesAsync();
        }
    }
}
