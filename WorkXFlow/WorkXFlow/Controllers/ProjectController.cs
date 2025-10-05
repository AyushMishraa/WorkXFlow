using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkXFlow.Dtos;
using WorkXFlow.Services.Interfaces;

namespace WorkXFlow.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
       private readonly IProjectService _projectService;

       public ProjectController(IProjectService projectService)
       {
                _projectService = projectService;
       }

        // Get: api/project
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProjectReadDto>>> GetAllProjects()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        // Get: api/project/{id}
        [HttpPost("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProjectReadDto>> GetProjectById (int id)
        {
            var project = await _projectService.GetProjectById(id);
            if(project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        // Post: api/project
        [HttpPost]
        public async Task<ActionResult<ProjectReadDto>> CreateProject(ProjectCreateDto dto)
        {
            var createdProject = await _projectService.CreateProjectAsync(dto);
            return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
        }

        // Put: api/project/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectReadDto>> UpdateProjectAsync(int id, ProjectUpdateDto dto)
        {
            var project = await _projectService.UpdateProjectAsync(id, dto);
            if (project == false)
            {
                return NotFound();
            }
            return Ok(project);
        }

        // Delete: api/project/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectAsync(int id)
        {
            var result = await _projectService.DeleteProjectAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
