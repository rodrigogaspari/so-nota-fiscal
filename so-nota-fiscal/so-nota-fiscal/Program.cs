using FluentAssertions.Common;
using FluentValidation;
using IdempotentAPI.Cache.DistributedCache.Extensions.DependencyInjection;
using IdempotentAPI.Extensions.DependencyInjection;
using MediatR;
using SoNotaFiscal.Application.Abstractions;
using SoNotaFiscal.Application.Middlewares;
using SoNotaFiscal.Application.SwaggerGen;
using SoNotaFiscal.Application.Validation;
using SoNotaFiscal.Infrastructure.Database;
using SoNotaFiscal.Infrastructure.Database.Repository;
using SoNotaFiscal.Infrastructure.Sqlite;
using StackExchange.Redis;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Sqlite, learn more about Sqlite at:
        // https://www.sqlite.org
        builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue("DatabaseName", "Data Source=so-nota-fiscal.sqlite") });
        builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

        // MediatR
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        // FluentValidation
        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        // Data
        builder.Services.AddScoped<DbSession>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Repositories
        builder.Services.AddScoped<ISaldoRepository, SaldoRepository>();
        builder.Services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();

        // Idempotency with IdempotentAPI, learn more about IdempotentAPI at
        // https://github.com/ikyriak/IdempotentAPI/blob/master/README.md 
        builder.Services.AddIdempotentAPI();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddIdempotentAPIUsingDistributedCache();

        //Redir (running in Docker 127.0.0.1:6379)
        builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));

        // Customizations in Swagger (services)
        builder.Services.AddSwaggerSoNotaFiscalCustomizations();

        //Build App
        var app = builder.Build();
        
        // Customizations in Swagger (app)
        app.AddSwaggerSoNotaFiscalCustomizations();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        // Sqlite, learn more about Sqlite at:
        // https://www.sqlite.org
        app.Services.GetService<IDatabaseBootstrap>().Setup();

        app.UseErrorMiddleware();

        app.Run();
    }
}