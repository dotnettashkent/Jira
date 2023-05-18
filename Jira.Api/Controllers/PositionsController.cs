using Jira.Service.DTOs.Positions;
using Jira.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jira.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : BaseController
    {
        private readonly IPositionService positionService;
        public PositionsController(IPositionService positionService)
        {
            this.positionService = positionService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostPositionAsync(PositionCreationDto dto)
           => Ok(new
           {
               Code = 200,
               Error = "Success",
               Data = await this.positionService.CreateAsync(dto)
           });

        [HttpPut("update")]
        public async Task<IActionResult> PutPositionAsync(PositionUpdateDto dto)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.positionService.UpdateAsync(dto)
            });

        [HttpDelete("delete/{id:long}")]
        public async Task<IActionResult> DeletePositionAsync(long id)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.positionService.DeleteAsync(id)
            });

        [HttpGet("get-by-id/{id:long}")]
        public async Task<IActionResult> GetByIdAsync(long id)
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.positionService.GetByIdAsync(id)
            });

        [HttpGet("get-list")]
        public async Task<IActionResult> GetAllPositions()
            => Ok(new
            {
                Code = 200,
                Error = "Success",
                Data = await this.positionService.GetAllAsync()
            });
    }
}
