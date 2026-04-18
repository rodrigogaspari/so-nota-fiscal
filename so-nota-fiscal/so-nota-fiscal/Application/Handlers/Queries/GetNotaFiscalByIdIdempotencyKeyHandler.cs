using MediatR;
using SoNotaFiscal.Application.Abstractions;
using SoNotaFiscal.Application.Queries;
using SoNotaFiscal.Application.Queries.Responses;

namespace SoNotaFiscal.Application.Handlers.Queries
{
    public class GetNotaFiscalByIdIdempotencyKeyHandler : IRequestHandler<GetNotaFiscalByIdIdempotencyKeyQuery, ConsultaNotaFiscalResponse>
    {

        private readonly INotaFiscalRepository _notaFiscalRepository;

        public GetNotaFiscalByIdIdempotencyKeyHandler(ISaldoRepository saldoRepository, INotaFiscalRepository notaFiscalRepository)
        {
            _notaFiscalRepository = notaFiscalRepository;
        }

        public async Task<ConsultaNotaFiscalResponse> Handle(GetNotaFiscalByIdIdempotencyKeyQuery query, CancellationToken cancellationToken)
        {
            return _notaFiscalRepository.GetNotaFiscalByIdIdempotencyKey(query.IdIdempotencyKey);
        }
    }
}
