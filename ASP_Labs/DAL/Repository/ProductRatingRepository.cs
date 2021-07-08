using WebApp.DAL.Entities;
using WebApp.DAL.Interfaces.Database;

namespace WebApp.DAL.Repository
{
    public class ProductRatingRepository : Repository<ApplicationDbContext, ProductRating>, IProductRatingRepository
    {
        public ProductRatingRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
