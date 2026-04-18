using FluentValidation;
using SoNotaFiscal.Application.Abstractions;
using SoNotaFiscal.Application.Commands;

namespace SoNotaFiscal.Application.Commands.Requests.Validations
{
    public class EmitirNotaFiscalValidation : AbstractValidator<CreateNotaFiscalCommand>
    {
        private readonly INotaFiscalRepository _notaFiscalRepository;

        public EmitirNotaFiscalValidation(INotaFiscalRepository contaCorrenteRepository)
        {
            _notaFiscalRepository = contaCorrenteRepository;

            RuleFor(x => x.Valor)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Valor invalido para esta operacao.");

            RuleFor(x => x.Destinatario)
                .NotNull()
                .NotEmpty()
                .WithMessage("Destinatario invalido para esta operacao.");
        }
    }
}
