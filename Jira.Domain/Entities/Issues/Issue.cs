﻿using Jira.Domain.Commons;
using Jira.Domain.Entities.Companies;

namespace Jira.Domain.Entities.Issues
{
    public class Issue : Auditable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public long Code { get; set; }

        public long CategoryId { get; set; }
        public IssueCategory Category { get; set; }

        public long CompanyId { get; set; }
        public Company Company { get; set; }

        public long AssignedId { get; set; }
        public CompanyEmployee AssignedUser { get; set; }
    }
}
