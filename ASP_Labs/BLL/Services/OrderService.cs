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

        public async Task<ServiceResult> AddProductsToOrderAsync(ICollection<OrderItemDTO> orderItemsDTO)
        {
            var productIdList = orderItemsDTO.Select(x => x.ProductId).ToList();
            if (!_productRepository.CheckProductsExistence(productIdList))
            {
                return new ServiceResult(ServiceResultType.Bad_Request);
            }

            await _orderRepository.AddRangeAsync(orderItemsDTO.Select(_mapper.Map<Order>).ToList());

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<List<GameResponseDTO>>> SearchForOrderListByUserIdAsync(Guid userId)
        {
            var result = await _orderRepository.GetGamesByUserId(userId);

            return new ServiceResultClass<List<GameResponseDTO>>(result.Select(_mapper.Map<GameResponseDTO>).ToList(), ServiceResultType.Success);
        }

        public async Task<ServiceResultClass<List<GameResponseDTO>>> SearchForOrderListByOrdersIdAsync(ICollection<Guid> orderList)
        {
            var games = await _orderRepository.GetGamesByOrderId(orderList);

            return new ServiceResultClass<List<GameResponseDTO>>(games.Select(_mapper.Map<GameResponseDTO>).ToList(), ServiceResultType.Success);
        }

        public async Task<ServiceResult> BuySelectedItemsAsync(ICollection<Guid> orderList)
        {
            await _orderRepository.ChangeOrderStatusAsync(orderList, OrderStatus.Paid);

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<ServiceResult> RemoveSelectedItemsAsync(ICollection<Guid> orderList)
        {
            foreach (var orderId in orderList)
            {
                await _orderRepository.DeleteAsync(t => t.OrderId == orderId);
            }

            return new ServiceResult(ServiceResultType.Success);
        }

        public async Task<ServiceResult> SoftRemoveSelectedItemsAsync(ICollection<Guid> orderList)
        {
            await _orderRepository.ChangeOrderStatusAsync(orderList, OrderStatus.Rejected);

            return new ServiceResult(ServiceResultType.Success);
        }
    }
}
