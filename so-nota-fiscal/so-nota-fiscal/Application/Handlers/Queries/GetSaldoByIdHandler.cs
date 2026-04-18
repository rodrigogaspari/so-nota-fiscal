using MediatR;
using SoContaCorrente.Application.Abstractions;
using SoContaCorrente.Application.Queries;
using SoContaCorrente.Application.Queries.Responses;

namespace SoContaCorrente.Application.Handlers.Queries
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
