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

        public void RemoveOrderRange(List<Order> orders)
            => _dbContext.Orders.RemoveRange(orders);

        public async Task ChangeOrderStatusAsync(Guid orderId, OrderStatus orderStatus)
        {
            var order = new Order
            {
                OrderId = orderId,
                Status = orderStatus
            };

            _dbContext.Entry(order).Property(i => i.Status).IsModified = true;

            await _dbContext.SaveChangesAsync();
        }
    }
}
