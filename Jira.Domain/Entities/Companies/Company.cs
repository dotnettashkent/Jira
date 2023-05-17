using Jira.Domain.Commons;
using Jira.Domain.Entities.Issues;
using Jira.Domain.Entities.Users;

namespace Jira.Domain.Entities.Companies
{
    public class Company : Auditable
    {
        public string Name { get; set; }
        public long OwnerId { get; set; }
        public User Owner { get; set; }
        public ICollection<CompanyEmployee> Employees { get; set; }
        public ICollection<IssueCategory> IssueCategories { get; set; }
        public ICollection<Issue> Issues { get; set; }
    }
}
