using System.ComponentModel.DataAnnotations;

namespace WorkXFlow.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public string OwnerId { get; set; } // jisne project banaaya

        public ApplicationUser Owner { get; set; }  // navigation property

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; //kab create hua


        //for (1 project -> many tasks)
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

        //for (1 project -> many members)
        public ICollection<ProjectMember> Members { get; set; } = new List<ProjectMember>();
    }
}
