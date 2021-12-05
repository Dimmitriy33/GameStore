using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.BLL.Helpers;
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
            var param = new SqlParameter("@count", count);
            var result = await DbHelper.ExecuteSP(
                _dbContext.Database.GetConnectionString(),
                $"GetTopPopularPlatforms",
                new List<SqlParameter>() { param },
                DbHelper.ReaderToPlatforms
                );
            /* var result = await _dbContext.Products
                 .AsNoTracking()
                 .OrderByDescending(t => t.DateCreated)
                 .GroupBy(t => t.Platform)
                 .OrderByDescending(t => t.Count())
                 .Select(t => t.Key)
                 .Take(count)
                 .ToListAsync();*/

            return result;
        }

        public async Task<List<Product>> GetProductByNameAsync(string term, int limit, int offset)
        {
            var termParam = new SqlParameter("@term", term);
            var limitParam = new SqlParameter("@limit", limit);
            var offsetParam = new SqlParameter("@offset", offset);
            var result = await DbHelper.ExecuteSP(
                _dbContext.Database.GetConnectionString(),
                $"GetProductByName",
                new List<SqlParameter>() { termParam, limitParam, offsetParam },
                DbHelper.ReaderToProduct
                );
            /*var result = await _dbContext.Products
                .AsNoTracking()
                .Where(t => EF.Functions.Like(t.Name, $"{term}%"))
                .Skip(offset)
                .Take(limit)
                .ToListAsync();*/

            return result;
        }

        public async Task<Product> GetGameByIdAsync(Guid id)
        {
            var param = new SqlParameter("@product_id", id);
            var result = await DbHelper.ExecuteSP(
                _dbContext.Database.GetConnectionString(),
                $"GetGameById",
                new List<SqlParameter>() { param },
                DbHelper.ReaderToProduct
                );
            return result.FirstOrDefault();
            /*return await _dbContext.Products.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);*/
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            var param = new SqlParameter("@product_id", id);
            await DbHelper.ExecuteSP<string>(
                _dbContext.Database.GetConnectionString(),
                $"ProductSoftDelete",
                new List<SqlParameter>() { param }
                );

            /*var item = new Product
            {
                Id = id,
                IsDeleted = true
            };

            _dbContext.Entry(item).Property(i => i.IsDeleted).IsModified = true;
            await _dbContext.SaveChangesAsync();*/
        }

        public async Task ChangeGameRatingAsync(Guid id)
        {
            var param = new SqlParameter("@product_id", id);
            await DbHelper.ExecuteSP<string>(
                _dbContext.Database.GetConnectionString(),
                $"ChangeProductAvgRating",
                new List<SqlParameter>() { param }
                );
            /* var game = new Product
             {
                 Id = id,
                 Ratings = await _dbContext.ProductRating.Where(pR => pR.ProductId == id).ToListAsync(),
             };

             game.TotalRating = game.Ratings.Average(g => g.Rating);

             _dbContext.Entry(game).Property(i => i.TotalRating).IsModified = true;

             await _dbContext.SaveChangesAsync();*/
        }

        public async Task<bool> CheckProductsExistence(ICollection<Guid> products)
        {
            var resGuids = new List<Guid>();
            foreach (var prodId in products)
            {
                var param = new SqlParameter("@product_id", prodId);
                var result = await DbHelper.ExecuteSP(
                    _dbContext.Database.GetConnectionString(),
                    $"CheckProductsExistence",
                    new List<SqlParameter>() { param },
                    DbHelper.ReaderToProductId
                );
                resGuids.AddRange(result);
            }
            return products.Count == resGuids.Count;
            /*var countOfNotExistentProducts = await _dbContext.Products.AsNoTracking().Where(x => products.Contains(x.Id)).CountAsync();
            return products.Count == countOfNotExistentProducts;*/
        }


    }
}
