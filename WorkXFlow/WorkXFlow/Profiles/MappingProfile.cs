using AutoMapper;
using WorkXFlow.Models;
using WorkXFlow.Dtos;

namespace WorkXFlow.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // project mapping
            CreateMap<Project, ProjectReadDto>();
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<ProjectUpdateDto, Project>();

            // task mapping
            CreateMap<TaskItem, TaskReadDto>();
            CreateMap<TaskCreateDto, TaskItem>();
            CreateMap<TaskUpdateDto, TaskItem>();
        }
    }
}
