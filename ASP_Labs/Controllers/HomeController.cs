using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;


namespace ASP_Labs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        [Route("~/api/[controller]/info")]
        [HttpGet]
        public string GetInfo() => JsonSerializer.Serialize("Hello world!");

    }
}
