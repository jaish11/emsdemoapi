using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace emsdemoapi.Data.Services
{
    public class RedisCacheService
    {
        private readonly IDistributedCache _cache;
        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task SetAsync<T>(string key, T value,int minutes = 5 )
        {
            var json = JsonSerializer.Serialize(value);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
            };
            await _cache.SetStringAsync(key, json, options);
        }
        public async Task<T?> GetAsync<T>(string key)
        {
            var jsonData = await _cache.GetStringAsync(key);
            if (jsonData == null)
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(jsonData);
        }
        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
