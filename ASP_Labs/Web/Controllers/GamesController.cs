using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using WebApp.BLL.Constants;
using WebApp.BLL.DTO;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;
using WebApp.Web.Filters;

namespace WebApp.Web.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        #region Constants

        private const int CountOfTopPlatforms = 3;

        #endregion

        #region Services

        private readonly IClaimsReader _claimsHelper;
        private readonly IProductService _productService;

        #endregion

        public GamesController(IProductService productService, IClaimsReader claimsHelper)
        {
            _productService = productService;
            _claimsHelper = claimsHelper;
        }

        /// <summary>
        /// Get top three popular platforms
        /// </summary>
        /// <response code="200">Found games successfully</response>
        /// <response code="404">Unable to find games</response>
        [HttpGet("top-platforms")]
        public async Task<ActionResult<List<string>>> GetTopPlatforms()
        {
            var platforms = await _productService.GetTopPlatformsAsync(CountOfTopPlatforms);

            return StatusCode((int)platforms.ServiceResultType, platforms.Result);
        }

        /// <summary>
        /// Search games by input string
        /// </summary>
        /// <param name="term">Search part</param>
        /// <param name="limit">Maximum number of received items</param>
        /// <param name="offset">Amount of items you may skip</param>
        /// <response code="200">Found games successfully</response>
        /// <response code="404">Unable to find games</response>
        [HttpGet("search")]
        public async Task<ActionResult<List<GameResponseDTO>>> SearchGamesByName([BindRequired, FromQuery] string term, [BindRequired, FromQuery] int limit, [BindRequired, FromQuery] int offset)
        {
            var games = await _productService.SearchGamesByNameAsync(term, limit, offset);

            return StatusCode((int)games.ServiceResultType, games.Result);
        }

        /// <summary>
        /// Get game by id
        /// </summary>
        /// <param name="id">Game id</param>
        /// <response code="200">Found game successfully</response>
        /// <response code="404">Unable to find games</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<GameResponseDTO>> GetGameById([BindRequired] Guid id)
        {
            var game = await _productService.GetGameByIdAsync(id);

            return StatusCode((int)game.ServiceResultType, game.Result);
        }

        /// <summary>
        /// Create game
        /// </summary>
        /// <param name="gameDTO">New game </param>
        /// <response code="201">Game created successfully</response>
        /// <response code="400">Unable to create game</response>
        [HttpPost]
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<ActionResult<GameResponseDTO>> CreateGame([BindRequired, FromForm] GameRequestDTO gameDTO)
        {
            var newGame = await _productService.CreateGameAsync(gameDTO);

            return StatusCode((int)HttpStatusCode.Created, newGame.Result);
        }

        /// <summary>
        /// Soft delete game by id(IsDeleted = true)
        /// </summary>
        /// <param name="id">game id</param>
        /// <response code="200">Soft elete game successfully</response>
        /// <response code="404">Unable to find game</response>
        [HttpDelete("soft-remove/id/{id}")]
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<ActionResult> SoftDeleteGameById([BindRequired] Guid id)
        {
            var result = await _productService.SoftDeleteGameAsync(id);

            if (result.ServiceResultType is ServiceResultType.Success)
            {
                return NoContent();
            }

            return StatusCode((int)result.ServiceResultType);
        }

        /// <summary>
        /// Delete game by id
        /// </summary>
        /// <param name="id">game id</param>
        /// <response code="200">Delete game successfully</response>
        /// <response code="404">Unable to find game</response>
        [HttpDelete("id/{id}")]
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<ActionResult> DeleteGameById([BindRequired] Guid id)
        {
            var result = await _productService.DeleteGameAsync(id);

            if (result.ServiceResultType is ServiceResultType.Success)
            {
                return NoContent();
            }

            return StatusCode((int)result.ServiceResultType);
        }

        /// <summary>
        /// Update game by id
        /// </summary>
        /// <param name="gameDTO">game for update</param>
        /// <response code="200">Update game successfully</response>
        /// <response code="404">Unable to find game</response>
        [HttpPut]
        [Authorize(Roles = RolesConstants.Admin)]
        public async Task<ActionResult<GameResponseDTO>> UpdateGame([BindRequired, FromForm] GameRequestDTO gameDTO)
        {
            var result = await _productService.UpdateGameAsync(gameDTO);

            return StatusCode((int)result.ServiceResultType, result.Result);
        }

        /// <summary>
        /// Sort and filter games by input parameters
        /// </summary>
        /// <param name="gameSelectionDTO">model with parameters for sort and filter</param>
        /// <param name="limit">Maximum number of received items</param>
        /// <param name="offset">Amount of items you may skip</param>
        /// <response code="200">Found games successfully</response>
        /// <response code="400">Invalid parameters</response>
        [HttpGet("list")]
        [ServiceFilter(typeof(GamesSelectionFilter))]
        public async Task<ActionResult<ProductRatingDTO>> FilterAndSortGames(
            [FromQuery] GameSelectionDTO gameSelectionDTO,
            [Range(0, 1000)] int offset = GamesSelectionConstants.DefaultOffset,
            [Range(0, 100)] int limit = GamesSelectionConstants.DefaultLimit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Models contains one or more invalid parameters");
            }

            var result = await _productService.SortAndFilterGamesAsync(gameSelectionDTO, offset, limit);

            return StatusCode((int)result.ServiceResultType, result.Result);
        }

        /// <summary>
        /// Add new ProductRating for Game by User
        /// </summary>
        /// <param name="productRatingRequestDTO">Product ating model</param>
        /// <response code="200">Add new ProductRating successfully</response>
        [HttpPost("rating")]
        [Authorize]
        public async Task<ActionResult<ProductRatingActionDTO>> EditRating([BindRequired] ProductRatingActionDTO productRatingRequestDTO)
        {
            var productRating = new ProductRating
            {
                UserId = _claimsHelper.GetUserId(User).Result,
                ProductId = productRatingRequestDTO.ProductId,
                Rating = productRatingRequestDTO.Rating
            };

            var result = await _productService.EditGameRatingByUserAsync(productRating);

            return StatusCode((int)result.ServiceResultType, result.Result);
        }
    }
}
