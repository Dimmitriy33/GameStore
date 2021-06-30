using System.Collections.Generic;
using System.Linq;
using WebApp.DAL.EF;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;
using WebApp.DAL.Interfaces.Database;

namespace WebApp.DAL.Repository
{
    public class ProductRepository : Repository<ApplicationDbContext, Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public List<Platforms> GetTopThreePopularPlatforms()
        {
            var result = _dbContext.Products.GroupBy(t => t.Platform).OrderByDescending(t => t.Count()).Select(t=>t.Key).Take(3).ToList();
            return result;
        }
    }
}
