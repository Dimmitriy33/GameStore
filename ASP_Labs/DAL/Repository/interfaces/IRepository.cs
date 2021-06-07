using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.DAL.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
