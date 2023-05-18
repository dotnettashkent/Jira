using System.ComponentModel.DataAnnotations;

namespace Jira.Service.DTOs.Users
{
    public class UserCreationDto
    {
        [Required]
        public string Firstname { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
