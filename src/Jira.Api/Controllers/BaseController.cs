using Microsoft.AspNetCore.Mvc;

namespace Jira.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}
