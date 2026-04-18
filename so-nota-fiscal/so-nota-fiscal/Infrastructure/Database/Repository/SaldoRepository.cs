using Dapper;
using SoNotaFiscal.Application.Abstractions;
using SoNotaFiscal.Application.Queries.Responses;

namespace SoNotaFiscal.Infrastructure.Database.Repository
{
    public class SaldoRepository : ISaldoRepository
    {
        private DbSession _session;

        public SaldoRepository(DbSession session)
        {
            _session = session;
        }

        public Task<ConsultaNotaFiscalResponse> GetSaldo(string idContaCorrente)
        {
            return Task.FromResult(_session.Connection.Query<ConsultaNotaFiscalResponse>(
                    @"
                    SELECT 
                     r.Numero
                    ,r.Nome
                    ,DATETIME('now') SaldoDataHora
                    ,Sum(r.Saldo) Saldo 
                    FROM 
                    (
                         SELECT 
                         c.Numero 
                        ,c.Nome
                        ,CASE WHEN (m.tipomovimento='C') 
	                          THEN m.valor
                              ELSE m.valor * -1 END 
                        as Saldo 
                    
                        FROM movimento m INNER JOIN  
                        contacorrente c on m.idcontacorrente = c.idcontacorrente 
                    
                        WHERE 
                        m.idcontacorrente=@idContaCorrente
                    )as r", new { idContaCorrente }, _session.Transaction).FirstOrDefault() ?? new ConsultaNotaFiscalResponse());
        }
    }
}
