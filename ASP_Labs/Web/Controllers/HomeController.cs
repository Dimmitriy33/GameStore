
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Web.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        [Route("info")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetInfo() => Ok("Hello world!");
    }
}
