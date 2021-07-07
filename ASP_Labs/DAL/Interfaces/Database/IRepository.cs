using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.DAL.Enums;

namespace WebApp.DAL.Interfaces.Database
{
    public interface IRepository<T> where T : class
    {
        Task<T> CreateAsync(T item);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
        Task<T> UpdateItemAsync(T item);
        Task DeleteAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> SortItemsAsync<TKey>(Expression<Func<T, TKey>> sort, OrderType orderType);
    }
}
