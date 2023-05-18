using System.ComponentModel.DataAnnotations;

namespace Jira.Service.DTOs.Positions
{
    public class PositionCreationDto
    {
        [Required]
        public string Name { get; set; }
    }
}
