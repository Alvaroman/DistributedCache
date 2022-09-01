using Domain.DistributedCache;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.DistributedCache;
public class MemoryCache<T> : IApplicationCache<T>
{
    private readonly IMemoryCache _memoryCache;
    public MemoryCache() => _memoryCache = new MemoryCache(new MemoryCacheOptions());
    public Task<T?> GetValue(Guid cachingId) => Task.FromResult(_memoryCache.TryGetValue(cachingId, out T? result) ? result : default);

    public Task SetValue(Guid cachingId, T value) => Task.FromResult(_memoryCache.Set(cachingId, value));
}
