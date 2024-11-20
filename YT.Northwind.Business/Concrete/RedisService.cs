
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using 
    
    
    
    
    Northwind.Business.Abstract;

namespace Northwind.Business.Concrete
{
    public class RedisService : IRedisService
    {   
        private readonly IDistributedCache _distributedCache;
      

        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public T GetData<T>(string key)
        {

            try
            {
                var data = _distributedCache.GetString(key);
                return string.IsNullOrEmpty(data) ? default : JsonSerializer.Deserialize<T>(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                return default;
            }
        }

  
        public T SetData<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            };
            _distributedCache.SetString(key, JsonSerializer.Serialize(data), options);
            return data;
        }


        public void Delete<T>(string key)
        {
            _distributedCache.Remove(key);
        }
    }
}
