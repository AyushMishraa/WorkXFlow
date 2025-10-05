using AutoMapper;
using WorkXFlow.Data;
using Microsoft.EntityFrameworkCore;
using WorkXFlow.Dtos;
using WorkXFlow.Models;
using WorkXFlow.Services.Interfaces;

namespace WorkXFlow.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        // Constructor
        public ProjectService (AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        // Create Project
        public async Task<ProjectReadDto> CreateProjectAsync(ProjectCreateDto dto)
        {
            var project = _mapper.Map<Project>(dto);
            _appDbContext.Projects.Add(project);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<ProjectReadDto>(project);
        }

        // Get ALL projects
        public async Task<IEnumerable<ProjectReadDto>> GetAllProjectsAsync()
        {
            var projects = await _appDbContext.Projects.Include(p => p.Tasks).ToListAsync();
            return _mapper.Map<IEnumerable<ProjectReadDto>>(projects);
        }

        // Get project by Id
        public async Task<ProjectReadDto?> GetProjectById(int id)
        {
            var project = await _appDbContext.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == id);
            if(project == null)
            {
                return null;
            }

            return _mapper.Map<ProjectReadDto>(project);
        }

        // update project
        public async Task<bool> UpdateProjectAsync (int id, ProjectUpdateDto dto)
        {
            var project = await _appDbContext.Projects.FindAsync(id);
            if( project == null)
            {
                return false;
            }

            _mapper.Map(dto, project); // update project entity with dto values
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        // delete project 
        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _appDbContext.Projects.FindAsync(id);
            if (project == null)
            {
                return false;
            }

            _appDbContext.Projects.Remove(project);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
