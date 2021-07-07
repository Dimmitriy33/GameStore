using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Models;
using WebApp.DAL.Enums;

namespace WebApp.BLL.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResultClass<List<Platforms>>> GetTopPlatformsAsync(int count);
        Task<ServiceResultClass<List<GameResponseDTO>>> SearchGamesByNameAsync(string term, int limit, int offset);
        Task<ServiceResult> DeleteGameAsync(Guid id);
        Task<ServiceResult> SoftDeleteGameAsync(Guid id);
        Task<ServiceResultClass<GameResponseDTO>> CreateGameAsync(GameRequestDTO gameDTO);
        Task<ServiceResultClass<GameResponseDTO>> GetGameByIdAsync(Guid id);
        Task<ServiceResultClass<GameResponseDTO>> UpdateGameAsync(GameRequestDTO gameDTO);
        Task<ServiceResultClass<List<GameResponceDTO>>> SortDescGamesByRatingAsync();
        Task<ServiceResultClass<List<GameResponceDTO>>> SortGamesByRatingAsync();
        Task<ServiceResultClass<List<GameResponceDTO>>> SortDescGamesByPriceAsync();
        Task<ServiceResultClass<List<GameResponceDTO>>> SortGamesByPriceAsync();
    }
}
