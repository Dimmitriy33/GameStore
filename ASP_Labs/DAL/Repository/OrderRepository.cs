using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL.Entities;
using WebApp.DAL.Enums;
using WebApp.DAL.Interfaces.Database;

namespace WebApp.DAL.Repository
{
    public class OrderRepository : Repository<ApplicationDbContext, Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<List<Product>> GetGamesByUserId(Guid userId)
        {
            var result = await _dbContext.Orders
                .AsNoTracking()
                .Where(g => g.UserId == userId)
                .Select(t => t.Product)
                .ToListAsync();
            return result;
        }

        public async Task<List<Product>> GetGamesByOrderIdAsync(Guid orderId)
        {
            var result = await _dbContext.Orders
                .AsNoTracking()
                .Where(g => g.OrderId == orderId)
                .Select(t => t.Product)
                .ToListAsync();

            return result;
        }

        public void RemoveOrderRange(List<Order> orders) => _dbContext.Orders.RemoveRange(orders);

        public async Task ChangeOrderStatusAsync(ICollection<Guid> orderList, OrderStatus orderStatus)
        {
            var orders = await _dbContext.Orders.AsNoTracking().Where(x => orderList.Contains(x.OrderId)).ToListAsync();

            foreach (var order in orders)
            {
                order.Status = orderStatus;
                _dbContext.Entry(order).Property(i => i.Status).IsModified = true;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetGamesByOrderId(ICollection<Guid> orderList)
            => await _dbContext.Orders.AsNoTracking().Where(x => orderList.Contains(x.OrderId)).Select(t => t.Product).ToListAsync();
    }
}
