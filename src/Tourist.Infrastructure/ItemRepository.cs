using SQLite;
using Tourist.Domain;

namespace Tourist.Infrastructure;
public class ItemRepository : IGenericRepository<Item>
{
    readonly SQLiteAsyncConnection _dbConnection;

    public ItemRepository(DatabaseConfig databaseConfig)
    {
        _dbConnection = new SQLiteAsyncConnection(databaseConfig.Database);
    }

    public async Task<Item> Insert(Item model)
    {
        await _dbConnection.InsertAsync(model);
        return model;
    }

    public async Task<List<Item>> SelectAll()
    {
        return (await _dbConnection.QueryAsync<Item>("SELECT Name FROM Item;")).ToList();
    }

    public async Task<Item> SelectById(int id)
    {
        return await _dbConnection.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Item> Update(Item model)
    {
        await _dbConnection.UpdateAsync(model);
        return model;
    }
}
