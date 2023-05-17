using Jira.Domain.Enums;

namespace Jira.Service.DTOs.Users
{
    public class UserResultDto
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public UserImageResultDto Image { get; set; }
    }
}
