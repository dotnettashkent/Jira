using Jira.Service.DTOs.CompanyEmployees;

namespace Jira.Service.DTOs.Issues
{
    public class IssueResultDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public long Code { get; set; }

        public long CategoryId { get; set; }
        public IssueCategoryResultDto Category { get; set; }

        public long AssignedId { get; set; }
        public CompanyEmployeeResultDto AssignedUser { get; set; }
    }
}
