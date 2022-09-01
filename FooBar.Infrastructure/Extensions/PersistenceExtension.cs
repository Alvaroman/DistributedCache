using FooBar.Domain.Repository;
using FooBar.Infrastructure.Adapters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FooBar.Infrastructure.Extensions {

    public static class PersistenceExtensions {
        public static IServiceCollection AddPersistence(this IServiceCollection svc) {
            svc.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return svc;
        }
    }
}