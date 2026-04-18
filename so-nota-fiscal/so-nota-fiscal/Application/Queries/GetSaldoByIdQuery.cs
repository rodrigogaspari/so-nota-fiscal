using MediatR;
using SoContaCorrente.Application.Queries.Responses;

namespace SoContaCorrente.Application.Queries
{
    public record GetSaldoByIdQuery(string? IdContaCorrente) : IRequest<ConsultaSaldoResponse>;
}
