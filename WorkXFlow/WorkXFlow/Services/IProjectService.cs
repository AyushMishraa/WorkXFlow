using WorkXFlow.Dtos;
using WorkXFlow.Models;

namespace WorkXFlow.Services.Interfaces
{
    public interface IProjectService
    {
       Task<ProjectReadDto> CreateProjectAsync(ProjectCreateDto dto);
       Task<IEnumerable<ProjectReadDto>> GetAllProjectsAsync();
       Task<ProjectReadDto?> GetProjectById(int id);
       Task<bool> UpdateProjectAsync (int id, ProjectUpdateDto dto);
       Task<bool> DeleteProjectAsync(int id);
    }
}
