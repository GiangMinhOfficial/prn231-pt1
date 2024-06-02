using ContentNegotiationDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContentNegotiationDemo.Controllers
{
    [Route("api/blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            var blogs = new List<Blog>();
            var blogPosts = new List<BlogPost>
            {
                new() {
                    Title = "Content negotiation in .net core",
                    MetaDescription = "Content negotiation is one of",
                    Published = true,
                }
            };

            blogs.Add(new Blog
            {
                Name = "GM's Blog",
                Description = "This is blog of minh",
                BlogPosts = blogPosts,
            });

            return Ok(blogs);
        }
    }
}
