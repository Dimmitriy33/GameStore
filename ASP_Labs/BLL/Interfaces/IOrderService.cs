using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.BLL.DTO;
using WebApp.BLL.Models;

namespace WebApp.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<ServiceResult> AddProductsToOrderAsync(ICollection<OrderItemDTO> orderItemsDTO);
        Task<ServiceResult> BuySelectedItemsAsync(ICollection<Guid> orderList);
        Task<ServiceResult> RemoveSelectedItemsAsync(ICollection<Guid> orderList);
        Task<ServiceResult<List<GameResponseDTO>>> SearchForOrderListByOrdersIdAsync(ICollection<Guid> orderList);
        Task<ServiceResult<List<GameResponseDTO>>> SearchForOrderListByUserIdAsync(Guid userId);
        Task<ServiceResult> SoftRemoveSelectedItemsAsync(ICollection<Guid> orderList);
    }
}
