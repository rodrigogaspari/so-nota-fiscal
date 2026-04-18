using Dapper;
using SoNotaFiscal.Application.Abstractions;
using SoNotaFiscal.Application.Abstractions.Model;
using SoNotaFiscal.Application.Queries.Responses;
using StackExchange.Redis;

namespace SoNotaFiscal.Infrastructure.Database.Repository
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        private DbSession _session;
        private readonly IDatabase _cache;

        public NotaFiscalRepository(DbSession session, IConnectionMultiplexer redis)
        {
            _session = session;
            _cache = redis.GetDatabase(); 
        }

        private int ContaNota()
        {
            return _session.Connection.Query<int>(
                @"SELECT 
                count(*)  
                   
                FROM notafiscal",
                _session.Transaction).FirstOrDefault();
        }

        public async void Save(INotaFiscalModel notafiscalModel)
        {
            int proximaNota = this.ContaNota() + 1;

            string numero = proximaNota.ToString("D9"); //quantidade de notas concatenada com zeros para formar um número de 9 dígitos para o número e chave da NF-e;

            string chave = $"4125127704461800662355100{numero}1660600402";

            notafiscalModel.Numero = numero;
            notafiscalModel.Chave = chave;


            await _session.Connection.ExecuteAsync(
                @"INSERT INTO notafiscal 
                (chave, numero, destinatario, valor, idempotencyKey)
                
                VALUES 
                (@Chave, @Numero, @Destinatario, @Valor, @IdempotencyKey);"
                , notafiscalModel);
        }


        public async Task<ConsultaNotaFiscalResponse> GetNotaFiscalByIdIdempotencyKey(string idempotencyKey)
        {
            string cacheKey = $"nota-fiscal:{idempotencyKey}";

            // 1. Tenta recuperar do cache
            var cached = await _cache.StringGetAsync(cacheKey);
            if (cached.HasValue)
            {
                return System.Text.Json.JsonSerializer.Deserialize<ConsultaNotaFiscalResponse>(cached);
            }

            // 2. Se não estiver no cache, consulta no banco
            var notafiscal = await _session.Connection.QueryFirstOrDefaultAsync<ConsultaNotaFiscalResponse>(
            @"SELECT 
              chave
             ,numero
             ,destinatario
             ,valor
             ,idempotencyKey                
                   
              FROM notafiscal
                
              WHERE idempotencyKey = @idempotencyKey;",
            new { idempotencyKey });

            // 3. Armazena no cache com TTL (ex.:  24 horas)
            if (notafiscal != null)
            {
                var json = System.Text.Json.JsonSerializer.Serialize(notafiscal);
                await _cache.StringSetAsync(cacheKey, json, TimeSpan.FromHours(24));
            }

            return notafiscal;
        }
    }

    public class NotaFiscalModel : INotaFiscalModel
    {
        public string Chave { get; set; }

        public string Numero { get; set; }

        public string Destinatario { get; set; }

        public decimal Valor { get; set; }

        public string IdempotencyKey { get; set; }
    }
}
