using Microsoft.AspNetCore.Mvc;
using MyPortfolio.API.Services;
using MyPortfolio.Shared.DTOs;
using RESTFulSense.Controllers;

namespace MyPortfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : RESTFulController
    {
        private readonly IProjectService projectService;

        public ProjectsController(IProjectService projectService)
        {
            this.projectService = projectService;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<ProjectDto>> GetAll()
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
        public ActionResult<ProjectDto> GetById(int id)
        {
            var project = projectService.GetById(id);

            if (project == null)
                return NotFound();

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
    }
}
