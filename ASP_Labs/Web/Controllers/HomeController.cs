
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Web.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        #region Services

        private readonly ILogger<HomeController> _logger;

        #endregion

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("info")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetInfo()
        {
            string GetInfoMessage = "Request GET /api/home/info";

            _logger.LogInformation(GetInfoMessage);
            return Ok(GetInfoMessage);
        }
    }
}
