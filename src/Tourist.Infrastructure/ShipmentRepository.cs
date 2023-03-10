using Tourist.Domain;
using SQLite;

namespace Tourist.Infrastructure;
public class ShipmentRepository : IShipmentRepository, IGenericRepository<ShipmentLineItem>
{
    readonly SQLiteAsyncConnection _dbConnection;

    public ShipmentRepository(DatabaseConfig databaseConfig)
    {
        _dbConnection = new SQLiteAsyncConnection(databaseConfig.Database);
    }
    
    public async Task<List<ShipmentLineItem>> SelectActiveByCustomer(Guid customerId)
    {
        return (await _dbConnection.QueryAsync<ShipmentLineItem>("SELECT FROM ShipmentLineItem WHERE CustomerId  = @customerId;", customerId)).ToList();
    }

    public async Task<List<ShipmentLineItem>> SelectDelivered(string shipmentNo)
    {
        return (await _dbConnection.QueryAsync<ShipmentLineItem>("SELECT  FROM ShipmentLineItem WHERE Id = @Id;", shipmentNo)).ToList();
    }

    public async Task<ShipmentLineItem> Insert(ShipmentLineItem model)
    {
        await _dbConnection.InsertAsync(model);
        return model;
    }
    public async Task<List<ShipmentLineItem>> SelectAll()
    {
        return (await _dbConnection.QueryAsync<ShipmentLineItem>("SELECT Name, Address FROM ShipmentLineItems;")).ToList();
    }

    public async Task<ShipmentLineItem> SelectById(int id)
    {
        return await _dbConnection.Table<ShipmentLineItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<ShipmentLineItem> Update(ShipmentLineItem model)
    {
        await _dbConnection.UpdateAsync(model);
        return model;
    }
}