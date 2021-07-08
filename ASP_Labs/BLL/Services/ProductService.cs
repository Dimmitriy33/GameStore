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

        public async Task<ServiceResultClass<List<GameResponseDTO>>> SearchGamesByNameAsync(string term, int limit, int offset)
        {
            var games = await _productRepository.GetProductByNameAsync(term, limit, offset);

            return new ServiceResultClass<List<GameResponseDTO>>(_mapper.Map<List<GameResponseDTO>>(games), ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<GameResponseDTO>> GetGameByIdAsync(Guid id)
        {
            var game = await _productRepository.GetGameByIdAsync(id);

            if(game is null)
            {
                return new ServiceResultClass<GameResponseDTO>(ServiceResultType.Not_Found);
            }

            return new ServiceResultClass<GameResponseDTO>(_mapper.Map<GameResponseDTO>(game), ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<GameResponseDTO>> CreateGameAsync(GameRequestDTO gameDTO)
        {
            var newGame = await GetGameResponceDTOFromGameRequestDTO(gameDTO, _productRepository.CreateAsync);

            return new ServiceResultClass<GameResponseDTO>(newGame, ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<GameResponseDTO>> UpdateGameAsync(GameRequestDTO gameDTO)
        {
            var game = await _productRepository.GetGameByIdAsync(gameDTO.Id);

            if (game is null)
            {
                return new ServiceResultClass<GameResponseDTO>(ServiceResultType.Not_Found);
            }

            var updatedGame = await GetGameResponceDTOFromGameRequestDTO(gameDTO, _productRepository.UpdateItemAsync);

            return new ServiceResultClass<GameResponseDTO>(updatedGame, ServiceResultType.Success);
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

        private async Task<GameResponseDTO> GetGameResponceDTOFromGameRequestDTO(GameRequestDTO gameDTO, Func<Product, Task<Product>> operation)
        {
            var product = _mapper.Map<Product>(gameDTO);

            product.Logo = await _cloudinaryService.UploadImage(gameDTO.Logo);
            product.Background = await _cloudinaryService.UploadImage(gameDTO.Background);

            var result = await operation(product);
            return _mapper.Map<GameResponseDTO>(result);
        }
    }
}
