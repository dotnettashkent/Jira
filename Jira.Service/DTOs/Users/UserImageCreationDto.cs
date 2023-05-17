using Microsoft.AspNetCore.Http;

namespace Jira.Service.DTOs.Users
{
    public class UserImageCreationDto
    {
        public IFormFile Image { get; set; }
        public long UserId { get; set; }
    }
}
