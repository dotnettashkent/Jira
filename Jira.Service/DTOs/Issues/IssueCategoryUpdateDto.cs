using System.ComponentModel.DataAnnotations;

namespace Jira.Service.DTOs.Issues
{
    public class IssueCategoryUpdateDto
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public long CompanyId { get; set; }
    }
}
