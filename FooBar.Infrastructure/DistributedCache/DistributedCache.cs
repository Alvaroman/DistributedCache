using Domain.DistributedCache;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.DistributedCache;
public class DistribuitedCache<T> : IApplicationCache<T>
{
    private readonly IDistributedCache _distributedCache;
    private const int BACKUP_CACHING_DAYS = 7;
    public DistribuitedCache(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }
    public async Task<T?> GetValue(Guid cachingId)
    {
        var value = await _distributedCache.GetAsync(cachingId.ToString());
        return value is not null ? FromByteArray(value) : default;
    }

    public async Task SetValue(Guid cachingId, T value) => await _distributedCache.SetAsync(cachingId.ToString(), ToByteArray(value), BuildOptions());

    private T? FromByteArray(byte[]? data) => JsonSerializer.Deserialize<T?>(data);

    private byte[] ToByteArray(object? data) => JsonSerializer.SerializeToUtf8Bytes(data);

    private DistributedCacheEntryOptions BuildOptions() => new DistributedCacheEntryOptions()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(DateTime.Now.Date.AddDays(BACKUP_CACHING_DAYS).Subtract(DateTime.Now).TotalSeconds),
        SlidingExpiration = null
    };
}
