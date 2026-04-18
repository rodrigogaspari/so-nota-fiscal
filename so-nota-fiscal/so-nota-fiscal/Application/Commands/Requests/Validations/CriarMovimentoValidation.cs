using FluentValidation;
using SoNotaFiscal.Application.Abstractions;
using SoNotaFiscal.Application.Commands;

namespace SoNotaFiscal.Application.Queries.Requests.Validations
{
    public class CriarMovimentoValidation : AbstractValidator<CreateMovimentoCommand>
    {
        private readonly INotaFisscalRepository _notaFiscalRepository;

        public CriarMovimentoValidation(INotaFisscalRepository contaCorrenteRepository)
        {
            _notaFiscalRepository = contaCorrenteRepository;

            RuleFor(x => x.IdContaCorrente)
                .NotEmpty()
                .NotNull()
                .Must(IsValidAccount)
                .WithMessage("Conta inexistente.")
                .Must(IsAcctiveAccount)
                .WithMessage("Conta inativa para esta operacao.");

            RuleFor(x => x.Valor)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Valor invalido para esta operacao.");

            RuleFor(x => x.TipoMovimento)
                .NotNull()
                .NotEmpty()
                .Length(1)
                .Matches("[C|D]")
                .WithMessage("Tipo de movimento invalido para esta operacao.");
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
