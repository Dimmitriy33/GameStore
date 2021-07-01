using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;

namespace WebApp.Web.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        #region Services

        private readonly IProductService _productService;

        #endregion

        public GamesController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("top-platforms")]
        public async Task<IActionResult> GetTopPlatforms()
        {
            var platforms = await _productService.GetTopThreePlatforms();

            if (platforms.ServiceResultType is not ServiceResultType.Success)
            {
                return StatusCode((int)platforms.ServiceResultType);
            }

            return Ok(platforms.Result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetTopPlatforms([BindRequired, FromQuery]string term, [BindRequired, FromQuery] int limit, [BindRequired, FromQuery] int offset)
        {
            var games = await _productService.SearchGamesByName(term, limit,offset);

            if (games.ServiceResultType is not ServiceResultType.Success)
            {
                return StatusCode((int)games.ServiceResultType);
            }

            return Ok(games.Result);
        }
    }
}
