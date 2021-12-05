using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.BLL.Helpers;
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
            var param = new SqlParameter("@user_id", userId);
            var result = await DbHelper.ExecuteSP(
                _dbContext.Database.GetConnectionString(),
                $"GetGamesByUserId",
                new List<SqlParameter>() { param },
                DbHelper.ReaderToProduct
                );

            /* var result = await _dbContext.Orders
                 .AsNoTracking()
                 .Where(g => g.UserId == userId)
                 .Select(t => t.Product)
                 .ToListAsync();*/
            return result;
        }

        public async Task<List<Product>> GetGamesByOrderIdAsync(Guid orderId)
        {
            var param = new SqlParameter("@order_id", orderId);
            var result = await DbHelper.ExecuteSP(
                _dbContext.Database.GetConnectionString(),
                $"GetGamesByOrderId",
                new List<SqlParameter>() { param },
                DbHelper.ReaderToProduct
                );

            /* var result = await _dbContext.Orders
                 .AsNoTracking()
                 .Where(g => g.OrderId == orderId)
                 .Select(t => t.Product)
                 .ToListAsync();*/

            return result;
        }

        public void RemoveOrderRange(List<Order> orders)
        {
            foreach (var orderId in orders)
            {
                var param = new SqlParameter("@order_id", orderId.OrderId);
                DbHelper.ExecuteSPSync<string>(
                _dbContext.Database.GetConnectionString(),
                $"RemoveOrderById",
                new List<SqlParameter>() { param }
                );
            }
            /* _dbContext.Orders.RemoveRange(orders) */
        }
        public async Task RemoveOrderRangeByOrdersId(ICollection<Guid> orderList)
        {
            try
            {
                foreach (var orderId in orderList)
                {
                    var param = new SqlParameter("@order_id", orderId);
                    await DbHelper.ExecuteSP<string>(
                    _dbContext.Database.GetConnectionString(),
                    $"RemoveOrderById",
                    new List<SqlParameter>() { param }
                    );
                }
                /* var orders = await _dbContext.Orders.AsNoTracking().Where(x => orderList.Contains(x.OrderId)).ToListAsync();
                 _dbContext.Orders.RemoveRange(orders);

                 await _dbContext.SaveChangesAsync();*/
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Could not remove order in database. Error: {e.Message}");
            }
        }

        public async Task ChangeOrderStatusAsync(ICollection<Guid> orderList, OrderStatus orderStatus)
        {
            var paramStatus = new SqlParameter("@status", orderStatus);
            foreach (var orderId in orderList)
            {
                var param = new SqlParameter("@order_id", orderId);
                await DbHelper.ExecuteSP<string>(
                    _dbContext.Database.GetConnectionString(),
                    $"ChangeOrderStatus",
                    new List<SqlParameter>() { param, paramStatus }
                );
            }
            /*var orders = await _dbContext.Orders.AsNoTracking().Where(x => orderList.Contains(x.OrderId)).ToListAsync();

            foreach (var order in orders)
            {
                order.Status = orderStatus;
                _dbContext.Entry(order).Property(i => i.Status).IsModified = true;
            }

            await _dbContext.SaveChangesAsync();*/
        }

        public async Task<List<Product>> GetGamesByOrderId(ICollection<Guid> orderList)
        {
            var result = new List<Product>();
            foreach (var orderId in orderList)
            {
                var param = new SqlParameter("@order_id", orderId);
                var pList = await DbHelper.ExecuteSP(
                    _dbContext.Database.GetConnectionString(),
                    $"GetGamesByOrderId",
                    new List<SqlParameter>() { param },
                    DbHelper.ReaderToProduct
                    );
                result.AddRange(pList);
            }
            return result;
            /*return await _dbContext.Orders.AsNoTracking().Where(x => orderList.Contains(x.OrderId)).Select(t => t.Product).ToListAsync();*/
        }
    }
}
