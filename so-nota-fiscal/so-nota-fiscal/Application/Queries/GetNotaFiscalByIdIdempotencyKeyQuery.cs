using MediatR;
using SoNotaFiscal.Application.Queries.Responses;

namespace SoNotaFiscal.Application.Queries
{
    public record GetNotaFiscalByIdIdempotencyKeyQuery(string? IdIdempotencyKey) : IRequest<ConsultaNotaFiscalResponse>;
}
