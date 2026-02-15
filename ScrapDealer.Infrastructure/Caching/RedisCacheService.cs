using Microsoft.Extensions.Caching.Distributed;
using ScrapDealer.Application.Services;
using System.Text.Json;

namespace ScrapDealer.Infrastructure.Caching
{
    internal sealed class RedisCacheService : IRedisCacheService
    {
        private readonly IDistributedCache _cache;
        public RedisCacheService(IDistributedCache cache)
            => _cache = cache;

        public T? Get<T>(string key)
        {
            var cachedData = _cache.GetString(key);
            if (string.IsNullOrWhiteSpace(cachedData))
                return default;

            return JsonSerializer.Deserialize<T>(cachedData);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Set(string key, object value, TimeSpan expiration)
        {
            var serializedData = JsonSerializer.Serialize(value);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };
            _cache.SetString(key, serializedData, options);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var cachedData = await _cache.GetStringAsync(key);
            if (string.IsNullOrWhiteSpace(cachedData))
                return default;

            return JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var serializedData = JsonSerializer.Serialize(value);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            };
            await _cache.SetStringAsync(key, serializedData, options);
        }
    }

}
