using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;

namespace WebApp.BLL.Interfaces
{
    public interface IProductService
    {
        Task<ServiceResultClass<List<string>>> GetTopPlatformsAsync(int count);
        Task<ServiceResultClass<List<Product>>> SearchGamesByNameAsync(string term, int limit, int offset);
    }
}
