using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using ScrapDealer.Application.Services;

namespace ScrapDealer.Infrastructure.Caching
{
    internal sealed class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
            => _cache = cache;
        public T? Get<T>(string key)
            => _cache.TryGetValue(key, out T? value) ? value : default;

        public async Task<T?> GetAsync<T>(string key)
            => await Task.FromResult(_cache.TryGetValue(key, out T? value) ? value : default);

        public void Remove(string key)
            => _cache.Remove(key);

        public void Set(string key, object value, TimeSpan expiration)
            => _cache.Set(key, value, expiration);

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            _cache.Set(key, value, expiration);
            await Task.CompletedTask;
        }

    }
}
