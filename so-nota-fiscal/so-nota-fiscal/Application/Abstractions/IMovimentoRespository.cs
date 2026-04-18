using SoContaCorrente.Application.Abstractions.Model;

namespace SoContaCorrente.Application.Abstractions
{
    public interface IMovimentoRepository
    {
        public void Save(IMovimentoModel movimentoModel);
    }
}