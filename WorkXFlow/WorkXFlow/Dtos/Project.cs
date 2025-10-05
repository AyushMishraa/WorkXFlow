namespace WorkXFlow.Dtos
{
    public class ProjectCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ProjectUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ProjectReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // optional agr task k overview dena ho toh 
        public List<TaskReadDto> Tasks { get; set; }
    }

}
