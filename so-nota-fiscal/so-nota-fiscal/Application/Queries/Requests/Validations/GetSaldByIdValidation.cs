using FluentValidation;
using SoNotaFiscal.Application.Abstractions;

namespace SoNotaFiscal.Application.Queries.Requests.Validations
{
    public class GetSaldByIdValidation : AbstractValidator<GetSaldoByIdQuery>
    {
        private readonly INotaFisscalRepository _notaFiscalRepository; 

        public GetSaldByIdValidation(INotaFisscalRepository contaCorrenteRepository )
        {
            _notaFiscalRepository = contaCorrenteRepository;

            RuleFor(x => x.IdContaCorrente)
                .NotEmpty()
                .NotNull()
                .Must(IsValidAccount)
                .WithMessage("Conta inexistente.")
                .Must(IsAcctiveAccount)
                .WithMessage("Conta inativa para esta operacao.");
           
        }

        private bool IsValidAccount(string? idContaCorrente)
        {
            return _notaFiscalRepository.IsValidAccount(idContaCorrente);
        }

        private bool IsAcctiveAccount(string? idContaCorrente)
        {
            return _notaFiscalRepository.IsActiveAccount(idContaCorrente);
        }
    }
}
