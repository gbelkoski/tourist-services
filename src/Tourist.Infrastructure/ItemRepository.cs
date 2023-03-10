using System;
using SQLite;
using Tourist.Domain;

namespace Tourist.Infrastructure;
public class ItemRepository : IGenericRepository<Item>
{
    //readonly DatabaseConfig _databaseConfig;
    readonly SQLiteAsyncConnection _dbConnection;

    public ItemRepository(DatabaseConfig databaseConfig)
    {
        //_databaseConfig = databaseConfig;
        _dbConnection = new SQLiteAsyncConnection(databaseConfig.ConnectionString);
    }

    public async Task<Item> Insert(Item model)
    {
        //var connection = new SQLiteAsyncConnection(_databaseConfig.ConnectionString);

        await _dbConnection.InsertAsync(model);

        return model;
    }
    public async Task<List<Item>> SelectAll()
    {
        //var connection = new SQLiteAsyncConnection(_databaseConfig.ConnectionString);

        return (await _dbConnection.QueryAsync<Item>("SELECT Name FROM Item;")).ToList();
    }

    public async Task<Item> SelectById(int id)
    {
        //var connection = new SQLiteAsyncConnection(_databaseConfig.ConnectionString);
        return await _dbConnection.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Item> Update(Item model)
    {
        //var connection = new SQLiteAsyncConnection(_databaseConfig.ConnectionString);
        await _dbConnection.UpdateAsync(model);

        return model;
    }
}
