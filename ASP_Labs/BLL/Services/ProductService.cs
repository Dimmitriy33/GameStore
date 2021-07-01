using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;
using WebApp.DAL.Interfaces.Database;

namespace WebApp.BLL.Services
{
    public class ProductService : IProductService
    {
        //constants
        private const string NotFoundPlatforms = "Platforms not found";
        private const string NotFoundGames = "Games not found";

        //repositories
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ServiceResultClass<List<string>>> GetTopPlatformsAsync(int count)
        {
            var platforms = await _productRepository.GetTopPopularPlatformsAsync(count);

            if(platforms is null)
            {
                return new ServiceResultClass<List<string>>(NotFoundPlatforms,ServiceResultType.Internal_Server_Error);
            }

            return new ServiceResultClass<List<string>>(platforms.ConvertAll(p => p.ToString()), ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<List<Product>>> SearchGamesByNameAsync(string term, int limit, int offset)
        {
            var games = await _productRepository.GetProductByNameAsync(term, limit, offset);

            if(games is null)
            {
                return new ServiceResultClass<List<Product>>(NotFoundGames, ServiceResultType.Internal_Server_Error);
            }

            return new ServiceResultClass<List<Product>>(games, ServiceResultType.Success);
        }
    }
}
