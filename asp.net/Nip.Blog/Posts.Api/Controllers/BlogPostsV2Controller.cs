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
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/BlogPosts")]
    [ApiController]
    public class BlogPostsV2Controller : ControllerBase
    {

        private readonly ILogger<BlogPostsV2Controller> _logger;
        private readonly IBlogPostRepository _postsRepo;
        public BlogPostsV2Controller(ILogger<BlogPostsV2Controller> logger, IBlogPostRepository repo)
        {
            _logger = logger;
            _postsRepo = repo;
        }

        // GET api/v2/blogposts[?pageIndex=3&pageSize=10]
        [HttpGet]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BlogPost>))]
        [ProducesResponseType(200, Type = typeof(PaginatedItems<BlogPost>))]
        public async Task<IActionResult> Get([FromQuery]int pageIndex = -1, [FromQuery]int pageSize = 5)
        {
            var posts = await _postsRepo.GetAllAsync().ToList();
            if (pageIndex < 0)
            {
                return Ok(await _postsRepo.GetAllAsync().ToList());
            }
            else
            {
                PaginatedItems<BlogPost> pagedPosts = await _postsRepo.GetAllPagedAsync(pageIndex, pageSize);
                bool isLastPage = false;
                if (pageIndex >= pagedPosts.TotalItems / pageSize)
                    isLastPage = true;
                pagedPosts.NextPage = (!isLastPage ? Url.Link(null, new { pageIndex = pageIndex + 1, pageSize = pageSize }) : null);
                return Ok(pagedPosts);
            }
        }

        [HttpGet("withtitle/{title:minlength(1)}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(PaginatedItems<BlogPost>))]
        public async Task<IActionResult> Get(string title, [FromQuery]int pageIndex = 0, [FromQuery]int pageSize = 5)
        {
            var pagedPosts = await _postsRepo.GetAllPagedAsync(pageIndex, pageSize, x => x.Title.Contains(title));
            return Ok(pagedPosts);
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
            BlogPost post = await _postsRepo.GetAsync(id);
            if (post == null)
            {
                _logger.LogWarning("testk");
                throw new Exception("No posts atm");
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


        [ProducesResponseType(200, Type = typeof(BlogPostComment))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [HttpGet("{id}/comments", Name = "GetBlogPostComments")]
        public async Task<ActionResult<IEnumerable<BlogPostComment>>> GetAllComments(long id)
        {
            return Ok(await _postsRepo.GetCommentsAsync(id));
        }

        [ProducesResponseType(201, Type = typeof(BlogPostComment))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [HttpPost("{id}/comments")]
        public async Task PostComment(long id, [FromBody] BlogPostComment comment)
        {
            await _postsRepo.AddCommentAsync(id, comment);
        }
    }
}
