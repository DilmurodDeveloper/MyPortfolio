using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.API.Models;
using MyPortfolio.API.Services;
using MyPortfolio.Shared.DTOs;

namespace MyPortfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService projectService;

        public ProjectsController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var projects = projectService.GetAll();
            var result = projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                ProjectUrl = p.ProjectUrl,
                CreatedAt = p.CreatedAt
            });

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = projectService.GetById(id);
            if (project == null) return NotFound();

            var result = new ProjectDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                ImageUrl = project.ImageUrl,
                ProjectUrl = project.ProjectUrl,
                CreatedAt = project.CreatedAt
            };

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(ProjectRequestDto dto)
        {
            var project = new Project
            {
                Title = dto.Title,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                ProjectUrl = dto.ProjectUrl
            };

            projectService.Add(project);

            return CreatedAtAction(nameof(GetById), new { id = project.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProjectRequestDto dto)
        {
            var existing = projectService.GetById(id);
            if (existing == null) return NotFound();

            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.ImageUrl = dto.ImageUrl;
            existing.ProjectUrl = dto.ProjectUrl;

            projectService.Update(existing);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            projectService.Delete(id);
            return NoContent();
        }
    }
}
