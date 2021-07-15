using Microsoft.EntityFrameworkCore;
using System;
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
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<List<Platforms>> GetTopPopularPlatformsAsync(int count)
        {
            var result = await _dbContext.Products
                .AsNoTracking()
                .OrderByDescending(t => t.DateCreated)
                .GroupBy(t => t.Platform)
                .OrderByDescending(t => t.Count())
                .Select(t => t.Key)
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

        public async Task<Product> GetGameByIdAsync(Guid id) =>
            await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

        public async Task SoftDeleteAsync(Guid id)
        {
            var item = new Product
            {
                Id = id,
                IsDeleted = true
            };

            _dbContext.Entry(item).Property(i => i.IsDeleted).IsModified = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task ChangeGameRatingAsync(Guid id)
        {
            var game = new Product
            {
                Id = id,
                Ratings = await _dbContext.ProductRating.Where(pR => pR.ProductId == id).ToListAsync(),
            };

            game.TotalRating = game.Ratings.Average(g => g.Rating);

            _dbContext.Entry(game).Property(i => i.TotalRating).IsModified = true;

            await _dbContext.SaveChangesAsync();
        }

        public bool CheckProductsExistence(ICollection<Guid> products)
        {
            var countOfNotExistentProducts = _dbContext.Products.AsNoTracking().Where(x => products.Contains(x.Id)).Distinct().Count();
            if (products.Count == countOfNotExistentProducts)
            {
                return true;
            }

            return false;
        }
    }
}
