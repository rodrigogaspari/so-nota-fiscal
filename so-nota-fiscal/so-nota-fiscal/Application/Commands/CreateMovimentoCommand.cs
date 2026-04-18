using MediatR;

namespace SoNotaFiscal.Application.Commands
{
    public record CreateNotaFiscalCommand(string? Destinatario, decimal? Valor) : IRequest;
}
