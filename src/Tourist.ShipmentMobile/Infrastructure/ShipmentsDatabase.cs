using System;
using SQLite;
using Tourist.Domain;

namespace Tourist.ShipmentMobile.Infrastructure;
public class ShipmentsDatabase
{
    SQLiteAsyncConnection Database;

    public ShipmentsDatabase()
    {
    }

    async Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        var result = await Database.CreateTableAsync<Customer>();
    }

    public async Task<List<Customer>> GetItemsAsync()
    {
        await Init();
        return await Database.Table<Customer>().ToListAsync();
    }

    public async Task<List<Customer>> GetItemsNotDoneAsync()
    {
        throw new NotImplementedException();
        //await Init();
        //return await Database.Table<Customer>().Where(t => t.Done).ToListAsync();

        // SQL queries are also possible
        //return await Database.QueryAsync<Customer>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
    }

    public async Task<Customer> GetItemAsync(Guid id)
    {
        await Init();
        return await Database.Table<Customer>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(Customer item)
    {
        await Init();
        //if (item.Id ==)
        //    return await Database.UpdateAsync(item);
        //else
            return await Database.InsertAsync(item);
    }

    public async Task<int> DeleteItemAsync(Customer item)
    {
        await Init();
        return await Database.DeleteAsync(item);
    }
}
