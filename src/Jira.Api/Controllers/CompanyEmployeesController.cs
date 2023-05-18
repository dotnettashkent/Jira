using Jira.Service.DTOs.CompanyEmployees;
using Jira.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jira.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyEmployeesController : BaseController
    {
        private readonly ICompanyEmployeeService companyEmployeeService;
        public CompanyEmployeesController(ICompanyEmployeeService companyEmployeeService)
        {
            this.companyEmployeeService = companyEmployeeService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostEmployeeAsync(CompanyEmployeeCreationDto dto)
         => Ok(new
         {
             Code = 200,
             Error = "Success",
             Data = await this.companyEmployeeService.CreateAsync(dto)
         });

        [HttpPut("update")]
        public async Task<IActionResult> PutEmployeeAsync(CompanyEmployeeUpdateDto dto)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.companyEmployeeService.ModifyAsync(dto)
             });

        [HttpDelete("delete/{id:long}")]
        public async Task<IActionResult> DeleteEmployeeAsync(long id)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.companyEmployeeService.DeleteAsync(id)
             });

        [HttpGet("get-by-id/{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.companyEmployeeService.GetByIdAsync(id)
             });

        [HttpGet("get-list")]
        public async Task<IActionResult> GetAllEmployees(string search = null)
             => Ok(new
             {
                 Code = 200,
                 Error = "Success",
                 Data = await this.companyEmployeeService.GetAllAsync(search)
             });
    }
}
