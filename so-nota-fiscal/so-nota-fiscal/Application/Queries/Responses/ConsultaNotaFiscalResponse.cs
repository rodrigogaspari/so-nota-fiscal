namespace SoNotaFiscal.Application.Queries.Responses
{
    public class ConsultaNotaFiscalResponse
    {
        public int Numero { get; set; }

        public string Chave { get; set; }

        public string Destinatario { get; set; }

        public decimal Valor { get; set; }
    }
}
