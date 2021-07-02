using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;
using WebApp.DAL.Interfaces.Database;

namespace WebApp.BLL.Services
{
    public class ProductService : IProductService
    {
        #region Constants

        private const string NotFoundPlatforms = "Platforms not found";
        private const string NotFoundGames = "Games not found";

        #endregion

        #region Repositories

        private readonly IProductRepository _productRepository;

        #endregion

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ServiceResultClass<List<Platforms>>> GetTopPlatformsAsync(int count)
        {
            var platforms = await _productRepository.GetTopPopularPlatformsAsync(count);

            return new ServiceResultClass<List<Platforms>>(platforms, ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<List<Product>>> SearchGamesByNameAsync(string term, int limit, int offset)
        {
            var games = await _productRepository.GetProductByNameAsync(term, limit, offset);

            return new ServiceResultClass<List<Product>>(games, ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<Product>> GetGameByIdAsync(Guid id)
        {
            var game = await _productRepository.GetGameByIdAsync(id);

            if(game is null)
            {
                return new ServiceResultClass<Product>(ServiceResultType.Not_Found);
            }

            return new ServiceResultClass<Product>(game, ServiceResultType.Success);
        }
    }
}
