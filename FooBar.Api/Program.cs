using System.Reflection;
using Api.Filters;
using FooBar.Infrastructure.Context;
using FooBar.Infrastructure.Extensions;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add(typeof(AppExceptionFilterAttribute));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.Load("Application"), typeof(Program).Assembly);
builder.Services.AddAutoMapper(Assembly.Load("Application"));
builder.Services.AddPersistence();
builder.Services.AddCacheService(config, builder.Environment.IsDevelopment());

builder.Services.AddDbContext<PersistenceContext>(opt =>
{
    opt.UseSqlServer(config.GetConnectionString("database"), sqlopts =>
    {
        sqlopts.MigrationsHistoryTable("_MigrationHistory", config.GetValue<string>("SchemaName"));
    });
});

builder.Services.AddHealthChecks().AddSqlServer(config["ConnectionStrings:database"]);

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Block Api", Version = "v1" });
});

Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().CreateLogger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Block Api"));
}

app.UseRouting().UseHttpMetrics().UseEndpoints(endpoints =>
{
    endpoints.MapGet("/demo/cacheversion", () => new { version = 1.0, by = "Álvaro Morales" });
    endpoints.MapMetrics();
    endpoints.MapHealthChecks("/demo");
});

app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
