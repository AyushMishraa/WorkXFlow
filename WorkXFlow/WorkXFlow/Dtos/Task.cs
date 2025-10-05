namespace WorkXFlow.Dtos
{
    public class TaskCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public int ProjectId { get; set; } // ye zaroori hai taaki pata chale ki task kis project se belong karta hai
        public string AssignedToId { get; set; } //kisi user ko assign kiya ja sake
    }

    public class  TaskUpdateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // "ToDo", "InProgress", "Done"
    }

    public class TaskReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string status { get; set; }

        public int ProjectId { get; set; }
        public string AssignedToId { get; set; }
    }

}
