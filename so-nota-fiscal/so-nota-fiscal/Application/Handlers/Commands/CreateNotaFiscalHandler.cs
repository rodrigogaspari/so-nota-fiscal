using MediatR;
using SoNotaFiscal.Application.Abstractions;
using SoNotaFiscal.Application.Commands;
using SoNotaFiscal.Infrastructure.Database.Repository;

namespace SoNotaFiscal.Application.Handlers.Commands
{
    public class CreateNotaFiscalHandler : IRequestHandler<CreateNotaFiscalCommand>
    {

        private readonly INotaFiscalRepository _movimentoRepository;

        private readonly IUnitOfWork _unitOfWork;

        public CreateNotaFiscalHandler(INotaFiscalRepository movimentoRepository, IUnitOfWork unitOfWork)
        {
            _movimentoRepository = movimentoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateNotaFiscalCommand command, CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();

            _movimentoRepository.Save(
                new NotaFiscalModel()
                {
                    Destinatario = command.Destinatario,
                    Valor = command.Valor.GetValueOrDefault(), 
                    IdempotencyKey = command.IdempotencyKey,
                });

            _unitOfWork.Commit();

        }

    }
}
