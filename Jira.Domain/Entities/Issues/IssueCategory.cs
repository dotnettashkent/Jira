using Jira.Domain.Commons;
using Jira.Domain.Entities.Companies;

namespace Jira.Domain.Entities.Issues
{
    public class IssueCategory : Auditable
    {
        public string Name { get; set; }
        public long CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
