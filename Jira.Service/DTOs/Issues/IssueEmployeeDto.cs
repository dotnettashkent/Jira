namespace Jira.Service.DTOs.Issues
{
    public class IssueEmployeeDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public long Code { get; set; }
        public IssueCategoryResultDto Category { get; set; }
    }
}
