namespace Domain.DistributedCache;
public interface IApplicationCache<T>
{
    public Task<T?> GetValue(Guid cachingId);
    public Task SetValue(Guid cachingId, T value);
}
