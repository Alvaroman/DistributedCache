namespace Domain.DistributedCache;
public interface IApplicationCache<T>
{
    public Task<T?> GetValueAsync(Guid cachingId);
    public Task SetValueAsync(Guid cachingId, T value);
}
