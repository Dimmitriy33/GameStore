
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApp.Web.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger _logger;

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        [Route("info")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetInfo()
        {
            string GetInfoMessage = "Request GET /api/home/info";

            _logger.Information(GetInfoMessage);
            return Ok(GetInfoMessage);
        }
    }
}
