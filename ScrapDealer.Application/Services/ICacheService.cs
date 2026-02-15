namespace ScrapDealer.Application.Services
{
    public interface ICacheService
    {
        void Set(string key, object value, TimeSpan expiration);
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
        T? Get<T>(string key);
        Task<T?> GetAsync<T>(string key);
        void Remove(string key);
    }
}
