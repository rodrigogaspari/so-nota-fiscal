using Dapper;
using Microsoft.Data.Sqlite;

namespace SoNotaFiscal.Infrastructure.Sqlite
{
    public class DatabaseBootstrap : IDatabaseBootstrap
    {
        private readonly DatabaseConfig databaseConfig;

        public DatabaseBootstrap(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public void Setup()
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND (name = 'notafiscal');");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && (tableName == "notafiscal"))
                return;

            connection.Execute("CREATE TABLE notafiscal ( " +
                               "chave TEXT(44) PRIMARY KEY," +
                               "numero INTEGER(10) NOT NULL UNIQUE," +
                               "destinatario TEXT(100) NOT NULL," +
                               "valor REAL NOT NULL"+
                               ");");
        }

        public void EnsureDeleted()
        {
            using var connection = new SqliteConnection(databaseConfig.Name);

            FileInfo fi = new FileInfo(Path.Combine(Environment.CurrentDirectory, databaseConfig.DatabaseName));
            try
            {
                if (fi.Exists)
                {
                    connection.Close();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    fi.Delete();
                }
            }
            catch (Exception)
            {
                fi.Delete();
            }
        }
    }
}
