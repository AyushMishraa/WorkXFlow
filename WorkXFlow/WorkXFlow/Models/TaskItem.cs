using System.ComponentModel.DataAnnotations;

namespace WorkXFlow.Models
{
    public enum TaskStatus // Fixed typo: was 'TasKStatus'
    {
        ToDo,
        InProgress,
        Done
    }

    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        public TaskStatus Status { get; set; } = TaskStatus.ToDo;

        public DateTime? Deadline { get; set; }

        public int Priority { get; set; }  // 1 (high), 2 (medium), 3 (low)

        // Relations
        public int ProjectId { get; set; }
        public Project Project { get; set; }  // navigation property

        public string? AssignedToId { get; set; }  // jo is task ke liye responsible hai
        public ApplicationUser? AssignedUser { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
