using SoNotaFiscal.Application.Abstractions.Model;
using System.Xml.Linq;

namespace SoNotaFiscal.Application.Abstractions
{
    public interface INotaFiscalRepository
    {
        void Save(INotaFiscalModel movimentoModel);
    }
}