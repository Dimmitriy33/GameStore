using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace WebApp.BLL.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResult<List<Platforms>>> GetTopPlatformsAsync(int count);
        Task<ServiceResult<List<GameResponseDTO>>> SearchGamesByNameAsync(string term, int limit, int offset);
        Task<ServiceResult> DeleteGameAsync(Guid id);
        Task<ServiceResult> SoftDeleteGameAsync(Guid id);
        Task<ServiceResult<GameResponseDTO>> CreateGameAsync(GameRequestDTO gameDTO);
        Task<ServiceResult<GameResponseDTO>> GetGameByIdAsync(Guid id);
        Task<ServiceResult<GameResponseDTO>> UpdateGameAsync(GameRequestDTO gameDTO);
        Task<ServiceResult<List<GameResponseDTO>>> SortAndFilterGamesAsync(GameSelectionDTO gameSelection, int offset, int limit);
        Task<ServiceResult<ProductRatingDTO>> EditGameRatingByUserAsync(ProductRating productRating);
    }
}
