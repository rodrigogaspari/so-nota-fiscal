using MediatR;

namespace SoNotaFiscal.Application.Commands
{
    public record CreateMovimentoCommand(string? IdContaCorrente, string? TipoMovimento, decimal? Valor) : IRequest;
}
