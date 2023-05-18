using System.ComponentModel.DataAnnotations;

namespace Jira.Service.DTOs.Issues
{
    public class IssueCategoryCreationDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public long CompanyId { get; set; }
    }
}
