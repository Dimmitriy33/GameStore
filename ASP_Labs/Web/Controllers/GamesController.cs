using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.BLL.Interfaces;
using WebApp.DAL.Repository;

namespace WebApp.Web.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IProductService _productService;

        public GamesController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("top-platforms")]
        public async Task<IActionResult> TopPlatforms()
        {
            var platforms = _productService.GetTopThreePlatforms();

            if(platforms is null)
            {
                return StatusCode((int)platforms.ServiceResultType);
            }

            return Ok(platforms);
        }
    }
}
