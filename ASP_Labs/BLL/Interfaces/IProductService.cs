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
        Task<ServiceResultClass<List<GameResponceDTO>>> SearchGamesByNameAsync(string term, int limit, int offset);
        Task<ServiceResult> DeleteGameAsync(Guid id);
        Task<ServiceResult> SoftDeleteGameAsync(Guid id);
        Task<ServiceResultClass<GameResponceDTO>> CreateGameAsync(GameRequestDTO gameDTO);
        Task<ServiceResultClass<GameResponceDTO>> GetGameByIdAsync(Guid id);
        Task<ServiceResultClass<GameResponceDTO>> UpdateGameAsync(GameRequestDTO gameDTO);
    }
}
