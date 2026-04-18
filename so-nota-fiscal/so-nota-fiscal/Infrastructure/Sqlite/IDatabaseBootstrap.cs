namespace SoContaCorrente.Infrastructure.Sqlite
{
    public interface IDatabaseBootstrap
    {
        void Setup();

        void EnsureDeleted();
    }
}