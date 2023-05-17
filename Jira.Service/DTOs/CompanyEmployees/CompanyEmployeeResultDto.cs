using Jira.Domain.Enums;
using Jira.Service.DTOs.Companies;
using Jira.Service.DTOs.Issues;
using Jira.Service.DTOs.Positions;
using Jira.Service.DTOs.Users;

namespace Jira.Service.DTOs.CompanyEmployees
{
    public class CompanyEmployeeResultDto
    {
        public long Id { get; set; }
        public UserResultDto Employee { get; set; }

        public CompanyResultDto Company { get; set; }

        public PositionResultDto Position { get; set; }

        public UserPermission Permission { get; set; }
        public List<IssueEmployeeDto> Assignments { get; set; }
    }
}
