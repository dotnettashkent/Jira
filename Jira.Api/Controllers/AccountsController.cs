using Jira.Api.Models;
using Jira.Service.DTOs.Users;
using Jira.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jira.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;
        public AccountsController(IUserService userService, IAuthService authService)
        {
            this.userService = userService;
            this.authService = authService;
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> PostUserAsync(UserCreationDto dto)
           => Ok(new
           {
               Code = 200,
               Error = "Success",
               Data = await this.userService.CreateAsync(dto)
           });

        [HttpPost("generate-token")]
        public async Task<IActionResult> GenerateToken(string username, string password = null)
        {
            var token = await this.authService.GenerateTokenAsync(username, password);
            return Ok(new Response
            {
                Code = 200,
                Error = "Success",
                Data = token
            });
        }
    }
}
