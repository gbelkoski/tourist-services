using Dapper;
using Microsoft.Data.Sqlite;
using System.Linq;

namespace Tourist.Infrastructure;
public interface IDatabaseBootstrap
{
    void Setup();
}
public class DatabaseBootstrap : IDatabaseBootstrap
{
    readonly DatabaseConfig _databaseConfig;

    public DatabaseBootstrap(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public void Setup()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
 
        var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Customer';");
        var tableName = table.FirstOrDefault();
        if (!string.IsNullOrEmpty(tableName) && tableName == "Customer")
            return;
 
        connection.Execute(@"Create Table Customer (
            Id VARCHAR(20),
            Name VARCHAR(255) NOT NULL,
            Address VARCHAR(255) NULL);");
    }
}