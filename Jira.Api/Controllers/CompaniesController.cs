using Jira.Service.DTOs.Companies;
using Jira.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jira.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : BaseController
    {
        private readonly ICompanyService companyService;
        public CompaniesController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostCompanyAsync(CompanyCreationDto dto)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.companyService.CreateAsync(dto)
             });

        [HttpPut("update")]
        public async Task<IActionResult> PutCompanyAsync(CompanyUpdateDto dto)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.companyService.ModifyAsync(dto)
             });

        [HttpDelete("delete/{id:long}")]
        public async Task<IActionResult> DeleteCompany(long id)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.companyService.DeleteAsync(id)
             });

        [HttpGet("get-by-id/{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.companyService.GetByIdAsync(id)
             });

        [HttpGet("get-list")]
        public async Task<IActionResult> GetAllCompany(string search = null)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.companyService.GetAllAsync(search)
             });
    }
}
