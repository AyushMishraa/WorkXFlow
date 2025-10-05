using AutoMapper;
using WorkXFlow.Data;
using Microsoft.EntityFrameworkCore;
using WorkXFlow.Dtos;
using WorkXFlow.Models;
using System.Runtime.InteropServices;

namespace WorkXFlow.Services
{
    public class TaskService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TaskService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        // Create Task 
        public async Task<TaskReadDto> CreateTaskAsync(TaskCreateDto dto)
        {
            var task = _mapper.Map<TaskItem>(dto);
            _appDbContext.Tasks.Add(task);
            await _appDbContext.SaveChangesAsync();
            return _mapper.Map<TaskReadDto>(task);
        }

        // Get all tasks
        public async Task<IEnumerable<TaskReadDto>> GetAllTasksAsync()
        {
            var tasks = await _appDbContext.Tasks.Include(t => t.Project).ToListAsync();
            return _mapper.Map<IEnumerable<TaskReadDto>>(tasks);
        }

        // Get task by Id
        public async Task<TaskReadDto> GetTaskAsyncById(int id)
        {
            var task = await _appDbContext.Tasks.Include(t => t.Project).FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                return null;
            }
            return _mapper.Map<TaskReadDto>(task);
        }

        // update task
        public async Task<bool> UpdateTaskAsync(int id, TaskUpdateDto dto)
        {
            var task = await _appDbContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return false;
            }

            _mapper.Map(dto, task);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        // delete task
        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _appDbContext.Tasks.FindAsync(id);
            if (task == null)
            {
                return false;
            }
            _appDbContext.Tasks.Remove(task);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        // assign task to user
        public async Task<bool> AssignTaskToUserAsync(int taskId, string userId)
        {
            var task = await _appDbContext.Tasks.FindAsync(taskId);
            if (task == null)
            {
                return false;
            }

            var user = await _appDbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            task.AssignedToId = userId;

            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}
