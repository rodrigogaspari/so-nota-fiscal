using Dapper;
using SoNotaFiscal.Application.Abstractions;
using SoNotaFiscal.Application.Abstractions.Model;

namespace SoNotaFiscal.Infrastructure.Database.Repository
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        private DbSession _session;

        public NotaFiscalRepository(DbSession session)
        {
            _session = session;
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
                (chave, numero, destinatario, valor)
                
                VALUES 
                (@Chave, @Numero, @Destinatario, @Valor);"
                , notafiscalModel);
        }
               
    }

    public class NotaFiscalModel : INotaFiscalModel
    {
        public string Chave { get; set; }

        public string Numero { get; set; }

        public string Destinatario { get; set; }

        public decimal Valor { get; set; }
    }
}
