using System.ComponentModel.DataAnnotations;

namespace WorkXFlow.Models
{
    public enum ProjectRole
     {
        Manager, // full access , batayega ki user manager h 
        Member  // limited access, batayega ki user member h
    }
    public class ProjectMember
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }


        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        public ProjectRole Role { get; set; } = ProjectRole.Member;

    }
}


// ye junction table hai jo projects aur users ke beech many-to-many relationship ko represent karta hai