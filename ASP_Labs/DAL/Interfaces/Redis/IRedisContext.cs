using System;
using System.Threading.Tasks;

namespace WebApp.DAL.Interfaces.Redis
{
    public interface IRedisContext
    {
        Task<T> Get<T>(string key);
        Task Remove<T>(string key, TimeSpan? expiry = null);
        Task Set<T>(string key, T value, TimeSpan? expiry = null);
    }
}
