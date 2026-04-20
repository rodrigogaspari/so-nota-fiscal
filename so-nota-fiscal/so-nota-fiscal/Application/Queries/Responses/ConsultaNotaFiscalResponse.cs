namespace SoNotaFiscal.Application.Queries.Responses
{
    public class ConsultaNotaFiscalResponse
    {
        public string? IdentificacaoCliente { get; set; }

        public string? Cliente { get; set; }

        public string? Numero { get; set; }

        public decimal? Valor { get; set; }

        public string? Chave { get; set; }
    }
}
