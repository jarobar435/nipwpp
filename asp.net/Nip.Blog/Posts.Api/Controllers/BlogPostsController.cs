using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Posts.Api.Data;
using Posts.Api.Models;
using Microsoft.Extensions.Logging;

namespace Posts.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {

        private readonly BlogPostContext _postsDbContext;
        private readonly ILogger<Type> _logger;
        public BlogPostsController(BlogPostContext postsDbContext, ILogger<Type> logger)
        {
            _postsDbContext = postsDbContext;
            _logger = logger;
        }

        // GET api/blogposts
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(BlogPost))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<BlogPost>>> Get()
        {
            return Ok(await _postsDbContext.BlogPosts.ToAsyncEnumerable().ToList());
        } 

        // GET api/blogposts/5
        [HttpGet("{id}", Name = "GetBlogPost")]
        [ProducesResponseType(200, Type = typeof(BlogPost))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BlogPost>> Get(long id)
        {
            var item = await _postsDbContext.BlogPosts.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {

                return Ok(item);
            }
        }

        // PUT api/blogposts/5
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(BlogPost))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put(long id, [FromBody] BlogPost updatedPost)
        {
            var post = await _postsDbContext.BlogPosts.FindAsync(id);
            if (post == null)
            {
                _logger.LogWarning("testk");
                throw new Exception("No posts atm");
                return NotFound();
            }
            else
            {
                post.Title = updatedPost.Title;
                post.Description = updatedPost.Description;
                _postsDbContext.BlogPosts.Update(post);
                await _postsDbContext.SaveChangesAsync();
                return NoContent();
            }
        }

        // DELETE api/blogposts/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(BlogPost))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(long id)
        {
            var post = await _postsDbContext.BlogPosts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            else
            {
                _postsDbContext.BlogPosts.Remove(post);
                await _postsDbContext.SaveChangesAsync();
                return NoContent();
            }
        }

        // POST api/blogposts
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BlogPost))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Post([FromBody] BlogPost post)
        {
            await _postsDbContext.BlogPosts.AddAsync(post);
            await _postsDbContext.SaveChangesAsync();
            return CreatedAtRoute("GetBlogPost", new { id = post.Id }, post);
        }

        //    // GET api/values
        //    [HttpGet]
        //    public ActionResult<IEnumerable<string>> Get()
        //    {
        //        return new string[] { "value1", "value2" };
        //    }

        //    // GET api/values/5
        //    [HttpGet("{id}")]
        //    public ActionResult<string> Get(int id)
        //    {
        //        return "value";
        //    }

        //    // POST api/values
        //    [HttpPost]
        //    public void Post([FromBody] string value)
        //    {
        //    }

        //    // PUT api/values/5
        //    [HttpPut("{id}")]
        //    public void Put(int id, [FromBody] string value)
        //    {
        //    }

        //    // DELETE api/values/5
        //    [HttpDelete("{id}")]
        //    public void Delete(int id)
        //    {
        //    }
    }
}
