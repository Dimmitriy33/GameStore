using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.DAL.Interfaces.Database;

namespace WebApp.DAL.Repository
{
    public abstract class Repository<TContext, T> : IRepository<T>
        where TContext : ApplicationDbContext
        where T : class
    {
        protected readonly TContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public async Task<T> CreateAsync(T item)
        {
            try
            {
                await _dbSet.AddAsync(item);

                await _dbContext.SaveChangesAsync();

                _dbContext.Entry(item).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Could not create item in database. Error: {e.Message}");
            }

            return item;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            var count = expression == null
                ? await _dbSet.CountAsync()
                : await _dbSet.Where(expression).CountAsync();

            return count;
        }
        public async Task<T> UpdateItemAsync(T item)
        {
            try
            {
                _dbSet.Update(item);

                await _dbContext.SaveChangesAsync();

                _dbContext.Entry(item).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Unable to update item. Error: {e.Message}");
            }

            return item;
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> expression)
        {
            try
            {
                _dbSet.RemoveRange(_dbSet.Where(expression));

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception($"Unable to remove item or items. Error: {e.Message}");
            }
        }
    }
}
