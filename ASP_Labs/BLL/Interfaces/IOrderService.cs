using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Models;

namespace WebApp.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResult> AddProductsToOrderAsync(List<OrderItemDTO> orderItemsDTO);
        Task<ServiceResult> BuySelectedItemsAsync(List<Guid> orderList);
        Task<ServiceResult> RemoveSelectedItemsAsync(List<Guid> orderList);
        Task<ServiceResultClass<List<GameResponseDTO>>> SearchForOrderListByOrdersIdAsync(List<Guid> orderList);
        Task<ServiceResultClass<List<GameResponseDTO>>> SearchForOrderListByUserIdAsync(Guid userId);
        Task<ServiceResult> SoftRemoveSelectedItemsAsync(List<Guid> orderList);
    }
}
