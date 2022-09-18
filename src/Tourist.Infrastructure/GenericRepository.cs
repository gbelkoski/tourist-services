using SQLite;
using Tourist.Domain;

namespace Tourist.Infrastructure;
public class GenericRepository<T> : IGenericRepository<T>
{
    readonly SQLiteAsyncConnection _database;

    public GenericRepository()
    {
        var connectionString = "TouristServices.db";
        _database = new SQLiteAsyncConnection(connectionString);
        if(!_database.TableMappings.Any(c=>c.TableName=="Customer"))
            _database.CreateTableAsync<Customer>().Wait();
        if(!_database.TableMappings.Any(c=>c.TableName=="ShipmentLineItem"))
            _database.CreateTableAsync<ShipmentLineItem>().Wait();
    }

    public T Insert(T model)
    {
       _database.InsertAsync(model);
        return model;
    }

    public void Dispose()
    {
        _database.CloseAsync();
    }
 
    // public T Update<T>(T model)
    // {
    //     int iRes = Context.Update(model);
    //     return model;
    // }
 
    public async Task<bool> Delete<T>(object id)
    {
        int iRes = await _database.DeleteAsync<T>(id);
        return iRes.Equals(1);
    }
 
    // public T Select<T>(int pk) where T : new()
    // {
    //     var map = Context.GetMapping(typeof(T));
    //     return Context.Query<T>(map.GetByPrimaryKeySql, pk).First();
    // }
 
    // public void SelectAlls()
    // {
    //     Context.Table<People>().ToArray();
    // }
 
    public async Task<List<T>> SelectAll<T>() where T : new()
    {
        //return new TableQuery<T>(Context).ToArray();
        return await _database.QueryAsync<T>(string.Empty);
    }
}