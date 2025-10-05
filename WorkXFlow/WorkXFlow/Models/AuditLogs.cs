namespace WorkXFlow.Models
{
    public class AuditLogs
    {
        public int Id { get; set; }

        public string Action { get; set; } // e.g., "Created Task", "Updated Project"

        public string UserId { get; set; } // kisne action perform kiya

        public  ApplicationUser User { get; set; } // navigation property

        public DateTime Timestamp { get; set; } = DateTime.UtcNow; // kab action hua

        public string? Details { get; set; } // additional info about the action
    }
}


// ye class audit logs ko represent karti hai, jisme system me hone wale important actions ka record rakha jata hai