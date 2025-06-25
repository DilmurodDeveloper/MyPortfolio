using Microsoft.AspNetCore.Mvc;
using MyPortfolio.API.Services;
using MyPortfolio.Shared.DTOs;
using RESTFulSense.Controllers;

namespace MyPortfolio.API.Controllers
{
    public class BlogController : RESTFulController
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            blogService = blogService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BlogPostDto>> GetAll()
        {
            var posts = blogService.GetAll();

            var result = posts.Select(p => new BlogPostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                PublishedAt = p.PublishedAt,
                ImageUrl = p.ImageUrl
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<BlogPostDto> GetById(int id)
        {
            var post = blogService.GetById(id);
            if (post == null) return NotFound();

            var result = new BlogPostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                PublishedAt = post.PublishedAt,
                ImageUrl = post.ImageUrl
            };

            return Ok(result);
        }
    }
}
