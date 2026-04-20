namespace SoNotaFiscal.Application.Abstractions.Model
{
    public interface INotaFiscalModel
    {
        public string Chave { get; set; }

        public string Numero { get; set; }

        public string Cliente { get; set; }

        public decimal Valor { get; set; }
    }
}
