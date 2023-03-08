using Tourist.Domain;
using SQLite;

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
        var connection = new SQLiteAsyncConnection(_databaseConfig.ConnectionString);

        await connection.ExecuteAsync("INSERT INTO ShipmentLineItem ()" +
            "VALUES ();", model);
        
        return model;
    }
    
    public async Task<List<ShipmentLineItem>> SelectActiveByCustomer(Guid customerId)
    {
        var connection = new SQLiteAsyncConnection(_databaseConfig.ConnectionString);
 
        return (await connection.QueryAsync<ShipmentLineItem>("SELECT FROM ShipmentLineItem WHERE CustomerId  = @customerId;", customerId)).ToList();
    }

    public async Task<List<ShipmentLineItem>> SelectDelivered(string shipmentNo)
    {
        var connection = new SQLiteAsyncConnection(_databaseConfig.ConnectionString);
 
        return (await connection.QueryAsync<ShipmentLineItem>("SELECT  FROM ShipmentLineItem WHERE Id = @Id;", shipmentNo)).ToList();
    }
}