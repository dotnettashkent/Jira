using Jira.Service.DTOs.Issues;
using Jira.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jira.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : BaseController
    {
        private readonly IIssueService issueService;
        public IssuesController(IIssueService issueService)
        {
            this.issueService = issueService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostIssueAsync(IssueCreationDto dto)
         => Ok(new
         {
             Code = 200,
             Error = "Success",
             Data = await this.issueService.CreateAsync(dto)
         });

        [HttpPut("update")]
        public async Task<IActionResult> PutIssueAsync(IssueUpdateDto dto)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.issueService.UpdateAsync(dto)
             });

        [HttpDelete("delete/{id:long}")]
        public async Task<IActionResult> DeleteIssueAsync(long id)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.issueService.DeleteAsync(id)
             });

        [HttpGet("get-by-id/{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.issueService.GetByIdAsync(id)
             });

        [HttpGet("get-list")]
        public async Task<IActionResult> GetAllIssues([FromQuery] string search = null)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.issueService.GetAllAsync(search)
             });
    }
}
