using Jira.Domain.Enums;

namespace Jira.Service.DTOs.CompanyEmployees
{
    public class CompanyEmployeeUpdateDto
    {
        public long Id { get; set; }
        public long EmployeeId { get; set; }
        public long CompanyId { get; set; }
        public long PositionId { get; set; }
        public UserPermission Permission { get; set; }
    }
}
