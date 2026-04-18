using SoNotaFiscal.Application.Queries.Responses;

namespace SoNotaFiscal.Application.Abstractions
{
    public interface ISaldoRepository
    {
        Task<ConsultaSaldoResponse> GetSaldo(string idContaCorrente);
    }
}