using System.Collections.Generic;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace WebApp.DAL.Interfaces.Database
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Platforms> GetTopThreePopularPlatforms();
    }
}
