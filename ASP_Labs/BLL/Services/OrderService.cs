using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Interfaces;
using WebApp.BLL.Models;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;
using WebApp.DAL.Interfaces.Database;

namespace WebApp.BLL.Services
{
    public class OrderService : IOrderService
    {
        #region Repositories

        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        #endregion

        #region Services

        private readonly IMapper _mapper;

        #endregion

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult> AddProductsToOrderAsync(List<OrderItemDTO> orderItemsDTO)
        {
            foreach (var orderItem in orderItemsDTO)
            {
                if (await _productRepository.GetGameByIdAsync(orderItem.ProductId) is null)
                {
                    return new ServiceResult("Unable to add one of products to order", ServiceResultType.Bad_Request);
                }
            }

            await _orderRepository.AddRangeAsync(orderItemsDTO.Select(_mapper.Map<Order>).ToList());

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<List<GameResponseDTO>>> SearchForOrderListByUserIdAsync(Guid userId)
        {
            var result = await _orderRepository.GetGamesByUserId(userId);

            return new ServiceResultClass<List<GameResponseDTO>>(result.Select(_mapper.Map<GameResponseDTO>).ToList(), ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<List<GameResponseDTO>>> SearchForOrderListByOrdersIdAsync(List<Guid> orderList)
        {
            var games = new List<Product>();

            foreach (var orderId in orderList)
            {
                games.AddRange(await _orderRepository.GetGamesByOrderIdAsync(orderId));
            }

            return new ServiceResultClass<List<GameResponseDTO>>(games.Select(_mapper.Map<GameResponseDTO>).ToList(), ServiceResultType.Success);
        }

        public async Task<ServiceResult> BuySelectedItemsAsync(List<Guid> orderList)
        {
            foreach (var orderId in orderList)
            {
                await _orderRepository.ChangeOrderStatusAsync(orderId, OrderStatus.Paid);
            }

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<ServiceResult> RemoveSelectedItemsAsync(List<Guid> orderList)
        {
            foreach (var orderId in orderList)
            {
                await _orderRepository.DeleteAsync(t => t.OrderId == orderId);
            }

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<ServiceResult> SoftRemoveSelectedItemsAsync(List<Guid> orderList)
        {
            foreach (var orderId in orderList)
            {
                await _orderRepository.ChangeOrderStatusAsync(orderId, OrderStatus.Rejected);
            }

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
