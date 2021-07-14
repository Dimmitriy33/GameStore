using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;

namespace WebApp.DAL.Interfaces.Database
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task ChangeOrderStatusAsync(ICollection<Guid> orderList, OrderStatus orderStatus);
        Task<List<Product>> GetGamesByOrderId(ICollection<Guid> orderList);
        Task<List<Product>> GetGamesByOrderIdAsync(Guid orderId);
        Task<List<Product>> GetGamesByUserId(Guid userId);
        void RemoveOrderRange(List<Order> orders);
    }
}
