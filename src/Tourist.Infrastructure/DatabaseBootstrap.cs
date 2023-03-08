//using Dapper;
//using Microsoft.Data.Sqlite;
using Tourist.Domain;
using SQLite;

namespace Tourist.Infrastructure;
public interface IDatabaseBootstrap
{
    void Setup();
}
public class DatabaseBootstrap : IDatabaseBootstrap
{
    readonly DatabaseConfig _databaseConfig;

    SQLiteAsyncConnection Database;

    public DatabaseBootstrap(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public async void Setup()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(
            _databaseConfig.ConnectionString);

        var result = await Database.CreateTablesAsync<Customer, ShipmentLineItem, Item>();

        // This code is using dapper and Microsoft.Sqlite packages
        //using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        //var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'Customer';");
        //var tableName = table.FirstOrDefault();
        //if (!string.IsNullOrEmpty(tableName) && tableName == "Customer")
        //    return;

        //connection.Execute(@"Create Table Customer (
        //    Id INT,
        //    Name VARCHAR(255) NOT NULL,
        //    Address VARCHAR(255) NULL);");
    }
}