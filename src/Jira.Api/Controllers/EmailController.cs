using Jira.Service.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Jira.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailVerification emailverification;

        public EmailController(EmailVerification emailverification)
        {
            this.emailverification = emailverification;
        }

        [HttpPost]
        public async Task<IActionResult> SendVerificationCode(string email)
        {
            var result = await this.emailverification.SendAsync(email);
            return Ok(result);
        }
    }
}
