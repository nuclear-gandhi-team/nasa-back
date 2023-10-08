using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Nasa.BLL.ServicesContracts;

namespace Nasa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentFiresController : ControllerBase
    {
        private readonly ICurrentFiresService _currentFiresService;

        public CurrentFiresController(ICurrentFiresService currentFiresService)
        {
            _currentFiresService = currentFiresService;
        }

        [HttpGet("current-fires")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _currentFiresService.GetCurrentFires(DateTime.Now, 1));
        }
    }
}
