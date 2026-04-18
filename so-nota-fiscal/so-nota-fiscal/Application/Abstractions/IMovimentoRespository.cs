using SoNotaFiscal.Application.Abstractions.Model;

namespace SoNotaFiscal.Application.Abstractions
{
    public interface IMovimentoRepository
    {
        public void Save(IMovimentoModel movimentoModel);
    }
}