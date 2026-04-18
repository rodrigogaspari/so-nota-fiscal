using FluentAssertions.Common;
using FluentValidation;
using IdempotentAPI.Cache.DistributedCache.Extensions.DependencyInjection;
using IdempotentAPI.Extensions.DependencyInjection;
using MediatR;
using SoContaCorrente.Application.Abstractions;
using SoContaCorrente.Application.Middlewares;
using SoContaCorrente.Application.SwaggerGen;
using SoContaCorrente.Application.Validation;
using SoContaCorrente.Infrastructure.Database;
using SoContaCorrente.Infrastructure.Database.Repository;
using SoContaCorrente.Infrastructure.Sqlite;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Sqlite, learn more about Sqlite at:
        // https://www.sqlite.org
        builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue("DatabaseName", "Data Source=database.sqlite")});
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
        builder.Services.AddScoped<IMovimentoRepository, MovimentoRepository>();
        builder.Services.AddScoped<IContaCorrenteRepository, ContaCorrenteRepository>();

        // Idempotency with IdempotentAPI, learn more about IdempotentAPI at
        // https://github.com/ikyriak/IdempotentAPI/blob/master/README.md 
        builder.Services.AddIdempotentAPI();
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddIdempotentAPIUsingDistributedCache();

        // Customizations in Swagger (services)
        builder.Services.AddSwaggerSoContaCorrenteCustomizations();

        //Build App
        var app = builder.Build();
        
        // Customizations in Swagger (app)
        app.AddSwaggerSoContaCorrenteCustomizations();

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