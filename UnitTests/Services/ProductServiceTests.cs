using AutoMapper;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UnitTests.Constants;
using WebApp.BLL.Helpers;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Mappers;
using WebApp.BLL.Models;
using WebApp.BLL.Services;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;
using WebApp.DAL.Interfaces.Database;
using Xunit;

namespace UnitTests.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task Get_TopPlatforms_ReturnServiceResultWithListOfPlatforms()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();

            var count = 3;
            var platforms = new List<Platforms>
            {
                Platforms.Playstation,
                Platforms.Mobile,
                Platforms.Nintendo
            };

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            A.CallTo(() => productRepository.GetTopPopularPlatformsAsync(count)).Returns(platforms);

            //Act
            var result = await productService.GetTopPlatformsAsync(count);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
            Assert.Equal(platforms.Count, result.Result.Count);

            A.CallTo(() => productRepository.GetTopPopularPlatformsAsync(count)).MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineData("Br", 2, 0)]
        [InlineData("", 2, 0)]
        [InlineData("Brawl", 10, 10)]
        public async Task Search_GamesByName_ReturnServiceResultWithListOfGameResponseDTO(string term, int limit, int offset)
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();

            var gamesList = ProductConstants.ProductsList;

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            A.CallTo(() => productRepository.GetProductByNameAsync(term, limit, offset)).Returns(gamesList);

            //Act
            var result = await productService.SearchGamesByNameAsync(term, limit, offset);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
            Assert.InRange(result.Result.Count, default, limit);
            Assert.Equal(gamesList.Count, result.Result.Count);

            A.CallTo(() => productRepository.GetProductByNameAsync(term, limit, offset)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Get_GameByIdPositive_ReturnServiceResultWithGameResponseDTO()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            var product = ProductConstants.TestProduct1;

            A.CallTo(() => productRepository.GetGameByIdAsync(product.Id)).Returns(product);

            //Act
            var result = await productService.GetGameByIdAsync(product.Id);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
            Assert.Equal(product.Id, result.Result.Id);
            Assert.Equal(product.IsDeleted, result.Result.IsDeleted);
            Assert.Equal(product.TotalRating, result.Result.TotalRating);
            Assert.Equal(product.Rating, result.Result.Rating);
            Assert.Equal(product.Price, result.Result.Price);
            Assert.Equal(product.Platform, result.Result.Platform);
            Assert.Equal(product.Name, result.Result.Name);
            Assert.Equal(product.Logo, result.Result.Logo);
            Assert.Equal(product.Count, result.Result.Count);
            Assert.Equal(product.Genre, result.Result.Genre);
            Assert.Equal(product.Background, result.Result.Background);

            A.CallTo(() => productRepository.GetGameByIdAsync(product.Id)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Get_GameByIdNegative_ReturnServiceResultWithGameResponseDTO()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();

            var gameId = UserConstants.TestGuid1;

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            A.CallTo(() => productRepository.GetGameByIdAsync(gameId)).Returns((Product)null);

            //Act
            var result = await productService.GetGameByIdAsync(gameId);

            //Assert
            Assert.Equal(ServiceResultType.NotFound, result.ServiceResultType);

            A.CallTo(() => productRepository.GetGameByIdAsync(gameId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Create_GameByGameRequestDTOPositive_ReturnServiceResultWithGameResponseDTO()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();

            var gameRequestDTO = ProductConstants.TestGameRequestDTO;

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            //Act
            var result = await productService.CreateGameAsync(gameRequestDTO);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
        }

        [Fact]
        public async Task Create_GameByGameRequestDTONegative_ReturnServiceResultWithGameResponseDTO()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            //Act && Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => productService.CreateGameAsync(null));
        }

        [Fact]
        public async Task Update_GameByGameRequestDTOPositive_ReturnServiceResultWithGameResponseDTO()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();

            var gameRequestDTO = ProductConstants.TestGameRequestDTO;

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            //Act
            var result = await productService.UpdateGameAsync(gameRequestDTO);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task Update_GameByGameRequestDTONegative_ReturnServiceResultWithGameResponseDTO()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            //Act && Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => productService.UpdateGameAsync(null));
        }

        [Fact]
        public async Task Delete_GameByGameIdPositive_ReturnServiceResult()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();

            var gameId = UserConstants.TestGuid1;
            var product = ProductConstants.TestProduct1;

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            A.CallTo(() => productRepository.GetGameByIdAsync(gameId)).Returns(product);
            A.CallTo(() => productRepository.DeleteAsync(A<Expression<Func<Product, bool>>>._)).DoesNothing();

            //Act
            var result = await productService.DeleteGameAsync(gameId);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);

            A.CallTo(() => productRepository.GetGameByIdAsync(gameId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => productRepository.DeleteAsync(A<Expression<Func<Product, bool>>>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Delete_GameByGameIdNegative_ReturnServiceResult()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();

            var gameId = UserConstants.TestGuid1;

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            A.CallTo(() => productRepository.GetGameByIdAsync(gameId)).Returns((Product)null);

            //Act
            var result = await productService.DeleteGameAsync(gameId);

            //Assert
            Assert.Equal(ServiceResultType.NotFound, result.ServiceResultType);

            A.CallTo(() => productRepository.GetGameByIdAsync(gameId)).Returns((Product)null);
            A.CallTo(() => productRepository.DeleteAsync(A<Expression<Func<Product, bool>>>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task SoftDelete_GameByGameIdPositive_ReturnServiceResult()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();

            var gameId = UserConstants.TestGuid1;
            var product = ProductConstants.TestProduct1;
            product.Id = gameId;

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            A.CallTo(() => productRepository.GetGameByIdAsync(gameId)).Returns(product);
            A.CallTo(() => productRepository.SoftDeleteAsync(gameId)).DoesNothing();

            //Act
            var result = await productService.SoftDeleteGameAsync(gameId);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);

            A.CallTo(() => productRepository.GetGameByIdAsync(gameId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => productRepository.SoftDeleteAsync(gameId)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task SoftDelete_GameByGameIdNegative_ReturnServiceResult()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();

            var gameId = UserConstants.TestGuid1;

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            A.CallTo(() => productRepository.GetGameByIdAsync(gameId)).Returns((Product)null);

            //Act
            var result = await productService.SoftDeleteGameAsync(gameId);

            //Assert
            Assert.Equal(ServiceResultType.NotFound, result.ServiceResultType);

            A.CallTo(() => productRepository.GetGameByIdAsync(gameId)).MustHaveHappenedOnceExactly();
            A.CallTo(() => productRepository.SoftDeleteAsync(gameId)).MustNotHaveHappened();
        }

        [Fact]
        public async Task SortAndFilterGamesByGameSelectionModelWithPagination_ReturnServiceResult()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();
            var limit = 2;
            var offset = 0;
            var productsList = ProductConstants.ProductsList;

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            A.CallTo(() => productRepository.SortAndFilterItemsAsync(A<Expression<Func<Product, bool>>>._, A<Expression<Func<Product, object>>>._, limit, offset, A<OrderType>._)).Returns(productsList);

            //Act
            var result = await productService.SortAndFilterGamesAsync(ProductConstants.TestGameSelectionDTO, offset, limit);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);

            A.CallTo(() => productRepository.SortAndFilterItemsAsync(A<Expression<Func<Product, bool>>>._, A<Expression<Func<Product, object>>>._, limit, offset, A<OrderType>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Update_GameRatingByProductRating_ReturnServiceResultWithProductRatingDTO()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var productRatingRepository = A.Fake<IProductRatingRepository>();
            var cloudinaryService = A.Fake<ICloudinaryService>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<ProductRatingProfile>()).CreateMapper();
            var gameSelectionHelper = new GameSelectionHelper();

            var productRating = ProductRatingConstants.TestProductRating1;

            var productService = new ProductService(productRepository, mapper, cloudinaryService, gameSelectionHelper, productRatingRepository);

            A.CallTo(() => productRatingRepository.CreateAsync(productRating)).Returns(productRating);
            A.CallTo(() => productRepository.ChangeGameRatingAsync(productRating.ProductId)).DoesNothing();

            //Act
            var result = await productService.EditGameRatingByUserAsync(productRating);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
            Assert.NotNull(result.Result);
            Assert.Equal(productRating.ProductId, result.Result.ProductId);
            Assert.Equal(productRating.UserId, result.Result.UserId);
            Assert.Equal(productRating.Rating, result.Result.Rating);

            A.CallTo(() => productRatingRepository.CreateAsync(productRating)).MustHaveHappenedOnceExactly();
            A.CallTo(() => productRepository.ChangeGameRatingAsync(productRating.ProductId)).MustHaveHappenedOnceExactly();
        }
    }
}
