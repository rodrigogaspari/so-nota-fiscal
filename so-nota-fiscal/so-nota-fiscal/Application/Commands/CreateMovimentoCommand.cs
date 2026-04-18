using MediatR;

namespace SoContaCorrente.Application.Commands
{
    public record CreateMovimentoCommand(string? IdContaCorrente, string? TipoMovimento, decimal? Valor) : IRequest;
}
