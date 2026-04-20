using FluentAssertions;
using SoNotaFiscal.Application.Commands.Requests;
using SoNotaFiscal.Application.Queries.Responses;
using SoNotaFiscal.IntegrationTests.Factories;
using System.Net;
using System.Net.Http.Json;

namespace SoNotaFiscal.IntegrationTests.Controllers
{
    [Collection("Database")]
    public class NotaFiscalControllerTests : IClassFixture<SoNotaFiscalFactory>
    {
        private readonly SoNotaFiscalFactory _factory;

        public NotaFiscalControllerTests(SoNotaFiscalFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateMovimento_ShouldReturn_OK()
        {
            //Arrange
            var client = _factory.CreateClient();
            var guid = Guid.NewGuid().ToString();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("idempotencyKey", guid);

            //Act
            var request = new EmitirNotaFiscalRequest()
            {
                Cliente = "Carlos",
                Valor = 50
            };
            var response = await client.PostAsJsonAsync($"api/v1/notafiscal/nota", request);
                
            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task TryCreateMovimentoWithInvalidAmount_ShouldReturn_BadRequest_WithMessage_ValorInvalido()
        {
            // Arrange
            var client = _factory.CreateClient();
            var guid = Guid.NewGuid().ToString();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("idempotencyKey", guid);

            // Act
            var request = new EmitirNotaFiscalRequest()
            {
                Cliente = "Carlos",
                Valor = 0
            };

            var response = await client.PostAsJsonAsync($"api/v1/notafiscal/nota", request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Content.ReadAsStringAsync().Result.Should().Contain("Valor invalido para esta operacao.");
        }

    }
}
