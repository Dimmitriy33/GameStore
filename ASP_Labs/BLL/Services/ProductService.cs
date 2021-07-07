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
        #region Repositories

        private readonly IProductRepository _productRepository;

        #endregion

        #region Services

        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;

        #endregion

        private delegate Task<Product> Operation(Product item);
        private delegate Task<List<Product>> SortOperation(OrderType orderType);

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

        public async Task<ServiceResultClass<List<GameResponceDTO>>> SearchGamesByNameAsync(string term, int limit, int offset)
        {
            var games = await _productRepository.GetProductByNameAsync(term, limit, offset);

            return new ServiceResultClass<List<GameResponceDTO>>(_mapper.Map<List<GameResponceDTO>>(games), ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<GameResponceDTO>> GetGameByIdAsync(Guid id)
        {
            var game = await _productRepository.GetGameByIdAsync(id);

            if(game is null)
            {
                return new ServiceResultClass<GameResponceDTO>(ServiceResultType.Not_Found);
            }

            return new ServiceResultClass<GameResponceDTO>(_mapper.Map<GameResponceDTO>(game), ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<GameResponceDTO>> CreateGameAsync(GameRequestDTO gameDTO)
        {
            var newGame = await GetGameResponceDTOFromGameRequestDTO(gameDTO, _productRepository.CreateAsync);

            return new ServiceResultClass<GameResponceDTO>(newGame, ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<GameResponceDTO>> UpdateGameAsync(GameRequestDTO gameDTO)
        {
            var game = await _productRepository.GetGameByIdAsync(gameDTO.Id);

            if (game is null)
            {
                return new ServiceResultClass<GameResponceDTO>(ServiceResultType.Not_Found);
            }

            var updatedGame = await GetGameResponceDTOFromGameRequestDTO(gameDTO, _productRepository.UpdateItemAsync);

            return new ServiceResultClass<GameResponceDTO>(updatedGame, ServiceResultType.Success);
        }

        public async Task<ServiceResult> DeleteGameAsync(Guid id)
        {
            var game = await _productRepository.GetGameByIdAsync(id);

            if(game is null)
            {
                return new ServiceResult(ServiceResultType.Not_Found);
            }

            await _productRepository.DeleteAsync(g => g.Id == id);

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<ServiceResult> SoftDeleteGameAsync(Guid id)
        {
            var game = await _productRepository.GetGameByIdAsync(id);

            if(game is null)
            {
                return new ServiceResult(ServiceResultType.Not_Found);
            }

            await _productRepository.SoftDeleteAsync(game.Id);

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<List<GameResponceDTO>>> SortDescGamesByRatingAsync() 
            => await GetSortedGames(_productRepository.SortGamesByRatingAsync, OrderType.Desc);

        public async Task<ServiceResultClass<List<GameResponceDTO>>> SortGamesByRatingAsync() 
            => await GetSortedGames(_productRepository.SortGamesByRatingAsync, OrderType.Asc);

        public async Task<ServiceResultClass<List<GameResponceDTO>>> SortDescGamesByPriceAsync() 
            => await GetSortedGames(_productRepository.SortGamesByPriceAsync, OrderType.Desc);

        public async Task<ServiceResultClass<List<GameResponceDTO>>> SortGamesByPriceAsync()
            => await GetSortedGames(_productRepository.SortGamesByPriceAsync, OrderType.Asc);

        private async Task<GameResponceDTO> GetGameResponceDTOFromGameRequestDTO(GameRequestDTO gameDTO, Operation operation)
        {
            var product = _mapper.Map<Product>(gameDTO);

            product.Logo = await _cloudinaryService.UploadImage(gameDTO.Logo);
            product.Background = await _cloudinaryService.UploadImage(gameDTO.Background);

            var result = await operation.Invoke(product);
            return _mapper.Map<GameResponceDTO>(result);
        }

        private async Task<ServiceResultClass<List<GameResponceDTO>>> GetSortedGames(SortOperation sortOperation, OrderType orderType)
        {
            var games = await sortOperation(orderType);

            if (games is null)
            {
                return new ServiceResultClass<List<GameResponceDTO>>(ServiceResultType.Not_Found);
            }

            return new ServiceResultClass<List<GameResponceDTO>>(_mapper.Map<List<GameResponceDTO>>(games), ServiceResultType.Success);
        }
    }
}
