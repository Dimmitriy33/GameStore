using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace WebApp.DAL.Interfaces.Database
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetGameByIdAsync(Guid id);
        Task<List<Product>> GetProductByNameAsync(string term, int limit, int offset);
        Task<List<Platforms>> GetTopThreePopularPlatformsAsync();
    }
}
