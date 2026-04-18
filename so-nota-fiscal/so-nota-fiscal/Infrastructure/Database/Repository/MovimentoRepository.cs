using Dapper;
using SoNotaFiscal.Application.Abstractions;
using SoNotaFiscal.Application.Abstractions.Model;

namespace SoNotaFiscal.Infrastructure.Database.Repository
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private DbSession _session;

        public MovimentoRepository(DbSession session)
        {
            _session = session;
        }
        public async void Save(IMovimentoModel movimentoModel)
        {

            movimentoModel.IdMovimento = Guid.NewGuid().ToString();
            movimentoModel.DataMovimento = DateTime.Now;


            await _session.Connection.ExecuteAsync(
                @"INSERT INTO movimento 
                (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor)
                
                VALUES 
                (@IdMovimento, @IdContaCorrente, @DataMovimento, @TipoMovimento, @Valor);"
                , movimentoModel);
        }
    }

    public class MovimentoModel : IMovimentoModel
    {
        public string IdMovimento { get; set; }

        public string IdContaCorrente { get; set; }

        public DateTime DataMovimento { get; set; }

        public string TipoMovimento { get; set; }
            
        public decimal Valor { get; set; }
    }
}
