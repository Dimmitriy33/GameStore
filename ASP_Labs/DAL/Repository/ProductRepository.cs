using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using WebApp.DAL.Entities;
using WebApp.DAL.Enums;
using WebApp.DAL.Interfaces.Database;

namespace WebApp.DAL.Repository
{
    public class ProductRepository : Repository<ApplicationDbContext, Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) {}

        public async Task<List<Platforms>> GetTopPopularPlatformsAsync(int count)
        {
            var result = await _dbContext.Products
                .AsNoTracking()
                .OrderByDescending(t => t.DateCreated)
                .GroupBy(t => t.Platform)
                .OrderByDescending(t => t.Count())
                .Select(t=>t.Key)
                .Take(count)
                .ToListAsync();

            return result;
        }

        public async Task<List<Product>> GetProductByNameAsync(string term, int limit, int offset)
        {
            var result = await _dbContext.Products
                .AsNoTracking()
                .Where(t => EF.Functions.Like(t.Name, $"{term}%"))
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            return result;
        }
    }
}
