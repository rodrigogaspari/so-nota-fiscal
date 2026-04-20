using MediatR;

namespace SoNotaFiscal.Application.Commands
{
    public record CreateNotaFiscalCommand(string? Cliente, decimal? Valor, string IdempotencyKey) : IRequest;
}
