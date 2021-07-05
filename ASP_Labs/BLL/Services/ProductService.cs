using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
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

        #region Services

        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        #endregion

        public ProductService(IProductRepository productRepository, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
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

        public async Task<ServiceResultClass<Product>> CreateGameAsync(GameDTO gameDTO)
        {
            var product = _mapper.Map<Product>(gameDTO);
            
            product.Logo = await _cloudinaryService.UploadImage(gameDTO.Logo);
            product.Background = await _cloudinaryService.UploadImage(gameDTO.Background);
            product.DateCreated = DateTime.Now;

            var newGame = await _productRepository.CreateAsync(product);

            return new ServiceResultClass<Product>(newGame, ServiceResultType.Success);
        }

        public async Task<ServiceResult> DeleteGameAsync(Guid id)
        {
            var game = await _productRepository.GetGameByIdAsync(id);

            if(game is null)
            {
                return new ServiceResult(ServiceResultType.Not_Found);
            }

            await _productRepository.DeleteAsync(game);

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<ServiceResult> SoftDeleteGameAsync(Guid id)
        {
            var game = await _productRepository.GetGameByIdAsync(id);

            if(game is null)
            {
                return new ServiceResult(ServiceResultType.Not_Found);
            }

            await _productRepository.SoftDeleteAsync(game);

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<Product>> UpdateGameAsync(GameDTO gameDTO)
        {
            var mappedGame = _mapper.Map<Product>(gameDTO);
            var game = await _productRepository.GetGameByIdAsync(gameDTO.Id);

            if (game is null)
            {
                return new ServiceResultClass<Product>(ServiceResultType.Not_Found);
            }

            game.Logo = await _cloudinaryService.UploadImage(gameDTO.Logo);
            game.Background = await _cloudinaryService.UploadImage(gameDTO.Background);

            var updatedGame = await _productRepository.UpdateItemAsync(mappedGame);

            return new ServiceResultClass<Product>(updatedGame, ServiceResultType.Success);
        }
    }
}
