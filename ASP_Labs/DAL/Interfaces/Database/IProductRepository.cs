using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace WebApp.DAL.Interfaces.Database
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetProductByNameAsync(string term, int limit, int offset);
        Task<List<Platforms>> GetTopPopularPlatformsAsync(int count);
        Task<Product> GetGameByIdAsync(Guid id);
        Task SoftDeleteAsync(Guid id);
        Task ChangeGameRatingAsync(Guid id);
        Task<bool> CheckProductsExistence(ICollection<Guid> products);
    }
}
