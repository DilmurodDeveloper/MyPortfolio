using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.API.Services.Blogs;
using MyPortfolio.Shared.DTOs.Blogs;

namespace MyPortfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService blogService;

        public BlogsController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await blogService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await blogService.GetByIdAsync(id);
            return blog is null ? NotFound() : Ok(blog);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateBlogDto dto)
        {
            var id = await blogService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateBlogDto dto)
        {
            var ok = await blogService.UpdateAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await blogService.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
