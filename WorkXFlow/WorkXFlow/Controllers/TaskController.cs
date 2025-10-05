using Microsoft.AspNetCore.Mvc;
using WorkXFlow.Services.Interfaces;
using WorkXFlow.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace WorkXFlow.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
     
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // Get: api/task
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TaskReadDto>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        // GET: api/task/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TaskReadDto>> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskAsyncById(id);
            if (task == null) return NotFound();

            return Ok(task);
        }

        // Post: api/task
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<TaskReadDto>> CreateTask(TaskCreateDto taskDto)
        {
            var createdTask = await _taskService.CreateTaskAsync(taskDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        // PUT: api/task/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<TaskReadDto>> UpdateTask(int id, TaskUpdateDto taskDto)
        {
            var updatedTask = await _taskService.UpdateTaskAsync(id, taskDto);
            if (!updatedTask) return NotFound();

            return Ok(updatedTask);
        }

        // DELETE: api/task/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _taskService.DeleteTaskAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        // POST: api/task/{taskId}/assign/{userId}
        [HttpPost("task/{taskId}/assign/{userId}")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<ActionResult> AssignTaskToUser(int taskId, string userId)
        {
            var assigned = await _taskService.AssignTaskToUserAsync(taskId, userId);
            if (!assigned) return NotFound();
            return Ok("Task assigned successfully");
        }
    }
}


