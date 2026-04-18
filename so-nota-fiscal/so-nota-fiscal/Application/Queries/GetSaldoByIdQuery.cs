using MediatR;
using SoNotaFiscal.Application.Queries.Responses;

namespace SoNotaFiscal.Application.Queries
{
    public record GetSaldoByIdQuery(string? IdContaCorrente) : IRequest<ConsultaSaldoResponse>;
}
