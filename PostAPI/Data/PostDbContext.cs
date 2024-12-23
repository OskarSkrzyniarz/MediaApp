using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PostAPI.Models;

namespace PostAPI.Data
{
    public class PostDbContext : DbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext> options) : base(options) { }
        public DbSet<Post> Posts { get; set; }
    }
}
