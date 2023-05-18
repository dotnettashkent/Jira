using Jira.Service.DTOs.Companies;

namespace Jira.Service.DTOs.Issues
{
    public class IssueCategoryResultDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CompanyResultDto Company { get; set; }
    }
}
