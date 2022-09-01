using System;
using System.Collections.Generic;
using FooBar.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using FooBar.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FooBar.Domain.Repository;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Api.Tests;

class IntegrationTestBuilder : WebApplicationFactory<Program>
{

    readonly Guid _id;

    public Guid Id => this._id;

    public IntegrationTestBuilder()
    {
        _id = Guid.NewGuid();
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var rootDb = new InMemoryDatabaseRoot();

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<PersistenceContext>));
            services.AddDbContext<PersistenceContext>(options =>
                    options.UseInMemoryDatabase("Testing", rootDb));

        });

        SeedDatabase(builder.Build().Services);

        return base.CreateHost(builder);
    }

    void SeedDatabase(IServiceProvider services)
    {
        var languages = new List<CommonLanguage>
            {
                new CommonLanguage
                {
                    Id = _id, Name = "C#", Description = "This is a microsoft language.",
                    ReleasedIn = DateTime.Now.AddYears(-22)
                },
                new CommonLanguage
                {
                   Id = _id, Name = "Java", Description = "This is an oracle language.",
                    ReleasedIn = DateTime.Now.AddYears(-26)
                }
            };

        using (var scope = services.CreateScope())
        {
            var languageRepo = scope.ServiceProvider.GetRequiredService<IGenericRepository<CommonLanguage>>();
            foreach (var person in languages)
            {
                languageRepo.AddAsync(person).Wait();
            }
        }
    }
}