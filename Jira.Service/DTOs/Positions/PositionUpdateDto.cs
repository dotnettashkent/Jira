using System.ComponentModel.DataAnnotations;

namespace Jira.Service.DTOs.Positions
{
    public class PositionUpdateDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
