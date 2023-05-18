using Jira.Domain.Commons;
using Jira.Domain.Entities.Issues;
using Jira.Domain.Entities.Positions;
using Jira.Domain.Entities.Users;
using Jira.Domain.Enums;

namespace Jira.Domain.Entities.Companies
{
    public class CompanyEmployee : Auditable
    {

        public long EmployeeId { get; set; }
        public User Employee { get; set; }

        public long CompanyId { get; set; }
        public Company Company { get; set; }

        public long PositionId { get; set; }
        public Position Position { get; set; }

        public UserPermission Permission { get; set; }
        public ICollection<Issue> Assignments { get; set; }
    }
}
