using Tourist.Domain;
using Dapper;
using Microsoft.Data.Sqlite;

namespace Tourist.Infrastructure;
public class ShipmentRepository : IShipmentRepository
{
    readonly DatabaseConfig _databaseConfig;

    public ShipmentRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public async Task<ShipmentLineItem> Insert(ShipmentLineItem model)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        await connection.ExecuteAsync("INSERT INTO ShipmentLineItem ()" +
            "VALUES ();", model);
        
        return model;
    }
    
    public async Task<List<ShipmentLineItem>> SelectActiveByCustomer(Guid customerId)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
 
        return (await connection.QueryAsync<ShipmentLineItem>("SELECT FROM ShipmentLineItem WHERE CustomerId  = @customerId;", customerId)).ToList();
    }

    public async Task<List<ShipmentLineItem>> SelectDelivered(string shipmentNo)
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
 
        return (await connection.QueryAsync<ShipmentLineItem>("SELECT  FROM ShipmentLineItem WHERE Id = @Id;", shipmentNo)).ToList();
    }
}