﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;

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

        private readonly IProductService _productService;

        #endregion

        public GamesController(IProductService productService)
        {
            _productService = productService;
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
        /// /// <param name="term">Search part</param>
        /// /// <param name="limit">Maximum number of received items</param>
        /// /// <param name="offset">Amount of items you may skip</param>
        /// <response code="200">Found games successfully</response>
        /// <response code="404">Unable to find games</response>
        [HttpGet("search")]
        public async Task<ActionResult<List<Product>>> SearchGamesByName([BindRequired, FromQuery]string term, [BindRequired, FromQuery] int limit, [BindRequired, FromQuery] int offset)
        {
            var games = await _productService.SearchGamesByNameAsync(term, limit,offset);

            return StatusCode((int)games.ServiceResultType, games.Result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetGameById([BindRequired] Guid id)
        {
            var game = await _productService.GetGameByIdAsync(id);

            return StatusCode((int)game.ServiceResultType, game.Result);
        }

        [HttpDelete("Softdelete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> SoftDeleteGameById([BindRequired] Guid id)
        {
            var result = await _productService.SoftDeleteGameAsync(id);

            if(result.ServiceResultType is ServiceResultType.Success)
            {
                return NoContent();
            }

            return StatusCode((int)result.ServiceResultType);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteGameById([BindRequired] Guid id)
        {
            var result = await _productService.DeleteGameAsync(id);

            if (result.ServiceResultType is ServiceResultType.Success)
            {
                return NoContent();
            }

            return StatusCode((int)result.ServiceResultType);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateGameById([BindRequired] GameDTO gameDTO)
        {
            var result = await _productService.UpdateGameAsync(gameDTO);

            return StatusCode((int)result.ServiceResultType, result.Result);
        }
    }
}
