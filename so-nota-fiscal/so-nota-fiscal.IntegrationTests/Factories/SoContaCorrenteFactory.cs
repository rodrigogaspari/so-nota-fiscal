using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using SoContaCorrente.Infrastructure.Sqlite;
using SoContaCorrente.IntegrationTests.Fixtures;

namespace SoContaCorrente.IntegrationTests.Factories
{
    [Collection("Database")]
    public class SoContaCorrenteFactory : WebApplicationFactory<Program>
    {
        private readonly DbFixture _dbfixture;

        public SoContaCorrenteFactory(DbFixture dbFixture)
        {
            _dbfixture = dbFixture;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            //Faz a troca da Singleton que controla a configuração da database para a database de testes.
            builder.ConfigureServices(services =>
            {
               services.AddSingleton(new DatabaseConfig { Name = $"Data Source = { _dbfixture.DatabaseName }" });
            });
        }
    }
}
