using MediatR;
using SoNotaFiscal.Application.Abstractions;
using SoNotaFiscal.Application.Commands;
using SoNotaFiscal.Infrastructure.Database.Repository;

namespace SoNotaFiscal.Application.Handlers.Commands
{
    public class CreateMovimentoHandler : IRequestHandler<CreateMovimentoCommand>
    {

        private readonly IMovimentoRepository _movimentoRepository;

        private readonly IUnitOfWork _unitOfWork;

        public CreateMovimentoHandler(IMovimentoRepository movimentoRepository, IUnitOfWork unitOfWork)
        {
            _movimentoRepository = movimentoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateMovimentoCommand command, CancellationToken cancellationToken)
        {
            _unitOfWork.BeginTransaction();

            _movimentoRepository.Save(
                new MovimentoModel()
                {
                    IdContaCorrente = command.IdContaCorrente,
                    TipoMovimento = command.TipoMovimento,
                    Valor = command.Valor.GetValueOrDefault()
                });

            _unitOfWork.Commit();

        }

    }
}
