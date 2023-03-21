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
    readonly SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

    SQLiteAsyncConnection _dbConnection;

    public DatabaseBootstrap(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public async void Setup()
    {
        if (_dbConnection is not null)
            return;

        _dbConnection = new SQLiteAsyncConnection(_databaseConfig.Database, Flags);

        var result = await _dbConnection.CreateTablesAsync<Customer, ShipmentLineItem, Item>();
    }
}