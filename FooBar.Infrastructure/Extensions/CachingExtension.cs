using Domain.DistributedCache;
using Infrastructure.DistributedCache;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;
public static class CachingExtension
{
    public static IServiceCollection AddCacheService(this IServiceCollection services, IConfiguration config, bool IsDevelopment = true)
    {
        var cacheConfiguration = config.GetValue<string>("RedisServer");

        if (!IsDevelopment)
        {
            services.AddSingleton(typeof(IApplicationCache<>), typeof(DistribuitedCache<>));
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cacheConfiguration;
            });
        }
        else
        {
            services.AddSingleton(typeof(IApplicationCache<>), typeof(MemoryCache<>));
        }
        return services;
    }
}
