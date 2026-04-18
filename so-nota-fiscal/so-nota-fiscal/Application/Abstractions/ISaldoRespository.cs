using SoContaCorrente.Application.Queries.Responses;

namespace SoContaCorrente.Application.Abstractions
{
    public interface ISaldoRepository
    {
        Task<ConsultaSaldoResponse> GetSaldo(string idContaCorrente);
    }
}