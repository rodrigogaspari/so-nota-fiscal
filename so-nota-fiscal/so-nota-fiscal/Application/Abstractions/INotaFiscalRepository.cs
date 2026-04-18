using SoNotaFiscal.Application.Abstractions.Model;
using SoNotaFiscal.Application.Queries.Responses;

namespace SoNotaFiscal.Application.Abstractions
{
    public interface INotaFiscalRepository
    {
        void Save(INotaFiscalModel movimentoModel);

        ConsultaNotaFiscalResponse GetNotaFiscalByIdIdempotencyKey(string idIdempotencyKey);
    }
}