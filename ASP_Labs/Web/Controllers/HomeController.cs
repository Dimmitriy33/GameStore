using Microsoft.AspNetCore.Mvc;


namespace WebApp.Web.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        [Route("info")]
        [HttpGet]
        public string GetInfo() => "Hello world!";

    }
}
