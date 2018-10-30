using Microsoft.EntityFrameworkCore;
using Posts.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Posts.Api.Data
{
    public class BlogPostContext : DbContext
    {
        public BlogPostContext(DbContextOptions<BlogPostContext> options)
        : base(options)
        {
            // nop
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
    }
}
