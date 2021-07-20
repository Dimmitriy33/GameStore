﻿using AutoFixture;
using AutoMapper;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitTests.Constants;
using WebApp.BLL.DTO;
using WebApp.BLL.Mappers;
using WebApp.BLL.Models;
using WebApp.BLL.Services;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;
using WebApp.DAL.Interfaces.Database;
using Xunit;

namespace UnitTests.Services
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task Add_ProductsToOrderByCollectionOfOrderItemsDTOPositive_ReturnServiceResult()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var orderRepository = A.Fake<IOrderRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var checkResult = true;

            var orderService = new OrderService(orderRepository, productRepository, mapper);

            var orderItems = new List<OrderItemDTO>
            {
                new Fixture().Create<OrderItemDTO>(),
                new Fixture().Create<OrderItemDTO>()
            };

            A.CallTo(() => productRepository.CheckProductsExistence(A<List<Guid>>._)).Returns(checkResult);
            A.CallTo(() => orderRepository.AddRangeAsync(A<IEnumerable<Order>>._));
            //Act
            var result = await orderService.AddProductsToOrderAsync(orderItems);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);

            A.CallTo(() => orderRepository.AddRangeAsync(A<IEnumerable<Order>>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task Add_ProductsToOrderByCollectionOfOrderItemsDTONegative_ReturnServiceResult()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var orderRepository = A.Fake<IOrderRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();
            var checkResult = true;

            var orderService = new OrderService(orderRepository, productRepository, mapper);

            var orderItems = new List<OrderItemDTO>();

            A.CallTo(() => productRepository.CheckProductsExistence(new List<Guid>())).Returns(checkResult);

            //Act
            var result = await orderService.AddProductsToOrderAsync(orderItems);

            //Assert
            Assert.Equal(ServiceResultType.Bad_Request, result.ServiceResultType);

            A.CallTo(() => orderRepository.AddRangeAsync(A<IEnumerable<Order>>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task Search_OrderListByUserIdPositive_ReturnServiceResultClassWithListOfGameResponseDTO()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var orderRepository = A.Fake<IOrderRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();

            var orderService = new OrderService(orderRepository, productRepository, mapper);

            var productsList = new List<Product>
            {
                TestValues.TestProduct1,
                TestValues.TestProduct2
            };

            A.CallTo(() => orderRepository.GetGamesByUserId(TestValues.TestGuid1)).Returns(productsList);

            //Act
            var result = await orderService.SearchForOrderListByUserIdAsync(TestValues.TestGuid1);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
            Assert.Equal(productsList.Count, result.Result.Count);
        }

        [Fact]
        public async Task Search_OrderListByOrdersIdPositive_ReturnServiceResultClassWithListOfGameResponseDTO()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var orderRepository = A.Fake<IOrderRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();

            var orderService = new OrderService(orderRepository, productRepository, mapper);

            var productsList = new List<Product>
            {
                TestValues.TestProduct1,
                TestValues.TestProduct2
            };

            var ordersIdList = new List<Guid>
            {
                TestValues.TestGuid1,
                TestValues.TestGuid2
            };

            A.CallTo(() => orderRepository.GetGamesByOrderId(ordersIdList)).Returns(productsList);

            //Act
            var result = await orderService.SearchForOrderListByOrdersIdAsync(ordersIdList);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
            Assert.Equal(productsList.Count, result.Result.Count);
        }

        [Fact]
        public async Task BuySelectedItemsByOrdersIdList_ReturnServiceResult()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var orderRepository = A.Fake<IOrderRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();

            var orderService = new OrderService(orderRepository, productRepository, mapper);

            var ordersIdList = new List<Guid>
            {
                TestValues.TestGuid1,
                TestValues.TestGuid2
            };

            A.CallTo(() => orderRepository.ChangeOrderStatusAsync(ordersIdList, OrderStatus.Paid));

            //Act
            var result = await orderService.BuySelectedItemsAsync(ordersIdList);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
        }

        [Fact]
        public async Task Remove_SelectedItemsByOrdersIdListPositive_ReturnServiceResult()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var orderRepository = A.Fake<IOrderRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();

            var orderService = new OrderService(orderRepository, productRepository, mapper);

            var ordersIdList = new List<Guid>
            {
                TestValues.TestGuid1,
                TestValues.TestGuid2
            };

            A.CallTo(() => orderRepository.RemoveOrderRangeByOrdersId(ordersIdList)).DoesNothing();

            //Act
            var result = await orderService.RemoveSelectedItemsAsync(ordersIdList);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
        }

        [Fact]
        public async Task Remove_SelectedItemsByOrdersIdListNegative_ReturnServiceResult()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var orderRepository = A.Fake<IOrderRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();

            var orderService = new OrderService(orderRepository, productRepository, mapper);


            A.CallTo(() => orderRepository.RemoveOrderRangeByOrdersId(null)).Throws<ArgumentNullException>();

            //Act && Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => orderRepository.RemoveOrderRangeByOrdersId(null));
        }

        [Fact]
        public async Task SoftRemove_SelectedItemsByOrdersIdList_ReturnServiceResult()
        {
            //Arrange
            var productRepository = A.Fake<IProductRepository>();
            var orderRepository = A.Fake<IOrderRepository>();
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>()).CreateMapper();

            var orderService = new OrderService(orderRepository, productRepository, mapper);

            var ordersIdList = new List<Guid>
            {
                TestValues.TestGuid1,
                TestValues.TestGuid2
            };

            A.CallTo(() => orderRepository.ChangeOrderStatusAsync(ordersIdList, OrderStatus.Rejected));

            //Act
            var result = await orderService.SoftRemoveSelectedItemsAsync(ordersIdList);

            //Assert
            Assert.Equal(ServiceResultType.Success, result.ServiceResultType);
        }
    }
}
