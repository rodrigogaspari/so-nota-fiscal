using MediatR;
using SoNotaFiscal.Application.Abstractions;
using SoNotaFiscal.Application.Queries;
using SoNotaFiscal.Application.Queries.Responses;

namespace SoNotaFiscal.Application.Handlers.Queries
{
    public class GetSaldoByIdHandler : IRequestHandler<GetSaldoByIdQuery, ConsultaSaldoResponse>
    {

        private readonly ISaldoRepository _saldoRepository;

        public GetSaldoByIdHandler(ISaldoRepository saldoRepository)
        {
            _saldoRepository = saldoRepository;
        }

        public async Task<ConsultaSaldoResponse> Handle(GetSaldoByIdQuery query, CancellationToken cancellationToken)
        {
            return await _saldoRepository.GetSaldo(query.IdContaCorrente);
        }
    }
}
