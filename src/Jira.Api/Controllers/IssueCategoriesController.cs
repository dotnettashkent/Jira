using Jira.Service.DTOs.Issues;
using Jira.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jira.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueCategoriesController : BaseController
    {
        private readonly IIssueCategoryService issueCategoryService;
        public IssueCategoriesController(IIssueCategoryService issueCategoryService)
        {
            this.issueCategoryService = issueCategoryService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostIssueCategoryAsync(IssueCategoryCreationDto dto)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.issueCategoryService.CreateAsync(dto)
             });

        [HttpPut("update")]
        public async Task<IActionResult> PutIssueCategoryAsync(IssueCategoryUpdateDto dto)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.issueCategoryService.UpdateAsync(dto)
             });

        [HttpDelete("delete/{id:long}")]
        public async Task<IActionResult> DeleteIssueCategoryAsync(long id)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.issueCategoryService.DeleteAsync(id)
             });

        [HttpGet("get-by-id/{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.issueCategoryService.GetByIdAsync(id)
             });

        [HttpGet("get-list")]
        public async Task<IActionResult> GetAllIssueCategories()
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.issueCategoryService.GetAllAsync()
             });
    }
}
