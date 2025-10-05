using System.ComponentModel.DataAnnotations;

namespace WorkXFlow.Dtos
{
    public class RegisterDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Role { get; set; } = "User"; // Default role is "User"
    }

    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }

    public class AuthResponseDto
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
