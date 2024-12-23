using Microsoft.AspNetCore.Mvc;
using PostAPI.Data;
using PostAPI.Models;

namespace PostAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private readonly PostDbContext _context;

        public PostsController(PostDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Post> Get() => _context.Posts.ToList();

        [HttpPost]
        public IActionResult Create(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Create), new { id = post.Id }, post);
        }
    }
}
