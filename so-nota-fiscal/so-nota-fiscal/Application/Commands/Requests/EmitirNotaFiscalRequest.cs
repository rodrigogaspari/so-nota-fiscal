namespace SoNotaFiscal.Application.Commands.Requests
{
    [Serializable]
    public class EmitirNotaFiscalRequest
    {
        /// <summary>
        /// Destinatário da NF-e
        /// </summary>
        /// <example>José da Silva</example>
        public string? Cliente { get; set; }

        /// <summary>
        /// Valor em R$ da NF-e.
        /// </summary>
        /// <example>100,00</example>
        public decimal? Valor { get; set; }
    }
}
