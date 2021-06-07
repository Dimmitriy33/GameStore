using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace WebApp.Web.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        [Route("info")]
        [HttpGet]
        public string GetInfo() => JsonSerializer.Serialize("Hello world!");

    }
}
