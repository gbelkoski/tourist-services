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

    SQLiteAsyncConnection _dbConnection;

    public DatabaseBootstrap(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public async void Setup()
    {
        if (_dbConnection is not null)
            return;

        _dbConnection = new SQLiteAsyncConnection(_databaseConfig.Database);

        var result = await _dbConnection.CreateTablesAsync<Customer, ShipmentLineItem, Item>();
    }
}