
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApp.Web.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        //Constants
        private const string GetInfoMessage = "Request GET /api/home/info";

        [Route("info")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetInfo()
        {
            Log.Information(GetInfoMessage);
            return Ok(GetInfoMessage);
        }
    }
}
