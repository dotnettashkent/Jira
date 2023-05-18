using Jira.Service.DTOs.Users;

namespace Jira.Service.DTOs.Companies
{
    public class CompanyResultDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public UserResultDto Owner { get; set; }
    }
}
