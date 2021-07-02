using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace WebApp.BLL.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResultClass<List<Platforms>>> GetTopPlatformsAsync(int count);
        Task<ServiceResultClass<List<Product>>> SearchGamesByNameAsync(string term, int limit, int offset);
        Task<ServiceResultClass<Product>> GetGameByIdAsync(Guid id);
    }
}
