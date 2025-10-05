using WorkXFlow.Dtos;
using WorkXFlow.Models;

namespace WorkXFlow.Services.Interfaces
{
    public interface ITaskService
    {
         Task<TaskReadDto> CreateTaskAsync(TaskCreateDto dto);
         Task<IEnumerable<TaskReadDto>> GetAllTasksAsync();
         Task<TaskReadDto> GetTaskAsyncById(int id);
         Task<bool> UpdateTaskAsync(int id, TaskUpdateDto dto);
         Task<bool> DeleteTaskAsync(int id);
         Task<bool> AssignTaskToUserAsync(int taskId, string userId);
    }
}
