namespace SoNotaFiscal.Application.Abstractions
{
    public interface INotaFisscalRepository
    {
        bool IsValidAccount(string? idContaCorrente);

        bool IsActiveAccount(string? idContaCorrente);
    }
}