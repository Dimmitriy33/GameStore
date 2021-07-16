using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL.Interfaces.Redis;

namespace WebApp.DAL.Redis
{
    public class RedisContext : IRedisContext
    {
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly ILogger<RedisContext> _logger;

        public RedisContext(IRedisCacheClient redisCacheClient, ILogger<RedisContext> logger)
        {
            _redisCacheClient = redisCacheClient;
            _logger = logger;
        }

        public async Task<T> Get<T>(string key)
        {
            try
            {
                var hashResultSet = await Redis().HashGetAllAsync(key);
                var hashResult = hashResultSet.FirstOrDefault();

                return hashResult.Value != RedisValue.Null
                    ? JsonConvert.DeserializeObject<T>(hashResult.Value.ToString())
                    : default;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"The error was occured during getting string value from Redis by key: {key}");

                return default;
            }
        }

        public async Task Set<T>(string key, T value, TimeSpan? expiry = null)
        {
            try
            {
                var transaction = Redis().CreateTransaction();

                var result = transaction.HashSetAsync(
                    key,
                    new[]
                    {
                        new HashEntry(key, new RedisValue(JsonConvert.SerializeObject(value).ToString()))
                    }
                );

                if (expiry is not null)
                {
                    transaction.KeyExpireAsync(key, expiry);
                }

                await transaction.ExecuteAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"The error was occured during saving string value with {key} key in Redis");
            }
        }

        public async Task Remove<T>(string key, TimeSpan? expiry = null)
        {
            try
            {
                var transaction = Redis().CreateTransaction();

                var result = transaction.HashDeleteAsync(
                    key,
                    new RedisValue(JsonConvert.SerializeObject(Get<T>(key).Result).ToString())
                );

                await transaction.ExecuteAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"The error was occured during saving string value with {key} key in Redis");
            }
        }

        private IDatabase Redis()
            => _redisCacheClient.GetDbFromConfiguration().Database;
    }
}
