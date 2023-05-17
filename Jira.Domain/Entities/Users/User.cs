using Jira.Domain.Commons;
using Jira.Domain.Entities.Companies;
using Jira.Domain.Enums;

namespace Jira.Domain.Entities.Users
{
    public class User : Auditable
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public ICollection<Company> Companies { get; set; }
    }
}
