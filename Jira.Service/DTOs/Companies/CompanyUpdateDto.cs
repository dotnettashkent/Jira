using System.ComponentModel.DataAnnotations;

namespace Jira.Service.DTOs.Companies
{
    public class CompanyUpdateDto
    {
        [Required]
        public long Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required]
        public long OwnerId { get; set; }
    }
}
