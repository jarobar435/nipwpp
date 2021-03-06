﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Posts.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Posts.Api.Repositories
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> GetAsync(long id);
        IAsyncEnumerable<BlogPost> GetAllAsync();
        Task<PaginatedItems<BlogPost>> GetAllPagedAsync(int pageIndex, int pageSize, Expression<Func<BlogPost, bool>> filter = null);
        Task AddAsync(BlogPost post);
        Task UpdateAsync(BlogPost post);
        Task DeleteAsync(long id);
        Task<IEnumerable<BlogPostComment>> GetCommentsAsync(long blogPostId);
        Task AddCommentAsync(long blogPostId, BlogPostComment comment);
    }
}
