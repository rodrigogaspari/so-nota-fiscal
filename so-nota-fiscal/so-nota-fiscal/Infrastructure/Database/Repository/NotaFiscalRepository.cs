using Dapper;
using SoNotaFiscal.Application.Abstractions;

namespace SoNotaFiscal.Infrastructure.Database.Repository
{
    public class NotaFiscalRepository : INotaFisscalRepository
    {
        private DbSession _session;

        public NotaFiscalRepository(DbSession session)
        {
            _session = session;
        }

        public bool IsValidAccount(string? idContaCorrente)
        {
            return _session.Connection.QueryFirst<bool>(
                @"SELECT 
                count(0) as BIT 
                   
                FROM contacorrente c 

                WHERE c.idcontacorrente=@idContaCorrente", new { idContaCorrente }, _session.Transaction);
        }

        public bool IsActiveAccount(string? idContaCorrente)
        {
            return _session.Connection.Query<bool>(
                @"SELECT 
                ativo as BIT 
                   
                FROM contacorrente c 

                WHERE c.idcontacorrente=@idContaCorrente", new { idContaCorrente }, _session.Transaction).FirstOrDefault();
        }

    }
}
