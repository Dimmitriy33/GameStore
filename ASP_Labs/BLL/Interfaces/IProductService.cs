using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResultClass<Product>> GetGameByIdAsync(Guid id);
        Task<ServiceResultClass<List<string>>> GetTopThreePlatforms();
        Task<ServiceResultClass<List<Product>>> SearchGamesByName(string term, int limit, int offset);
    }
}
