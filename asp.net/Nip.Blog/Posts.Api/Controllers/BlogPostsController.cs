using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Posts.Api.Data;
using Posts.Api.Models;
using Microsoft.Extensions.Logging;
using Posts.Api.Repositories;

namespace Posts.Api.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/BlogPosts")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {

        private readonly ILogger<BlogPostsController> _logger;
        private readonly IBlogPostRepository _postsRepo;
        public BlogPostsController(ILogger<BlogPostsController> logger, IBlogPostRepository repo)
        {
            _logger = logger;
            _postsRepo = repo;
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
            return Ok(await _postsRepo.GetAllAsync().ToList());
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
            var item = await _postsRepo.GetAsync(id);
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

            throw new Exception("No posts atm");
            BlogPost post = await _postsRepo.GetAsync(id);
            if (post == null)
            {
                _logger.LogWarning("testk");
            }
            else
            {
                post.Title = updatedPost.Title;
                post.Description = updatedPost.Description;
                await _postsRepo.UpdateAsync(post);
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
            await _postsRepo.DeleteAsync(id);
            return NoContent();
        }

        // POST api/blogposts
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(BlogPost))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        public async Task Post([FromBody] BlogPost post)
        {
            await _postsRepo.AddAsync(post);
        }
    }
}
