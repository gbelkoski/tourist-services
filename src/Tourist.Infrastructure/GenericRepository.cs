using SQLite;
using Tourist.Domain;

namespace Tourist.Infrastructure;
public class GenericRepository<T> : IGenericRepository<T>
{
    readonly SQLiteAsyncConnection _database;

    public GenericRepository()
    {
        var connectionString = "Data Source=TouristServices.db;Version=3;";
        _database = new SQLiteAsyncConnection(connectionString);
        _database.CreateTableAsync<Customer>().Wait();
        _database.CreateTableAsync<ItemLedger>().Wait();
        _database.CreateTableAsync<Package>().Wait();
        _database.CreateTableAsync<ShippmentItem>().Wait();
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
 
    // public bool Delete<T>(T model)
    // {
    //     int iRes = Context.Delete(model);
    //     return iRes.Equals(1);
    // }
 
    // public T Select<T>(int pk) where T : new()
    // {
    //     var map = Context.GetMapping(typeof(T));
    //     return Context.Query<T>(map.GetByPrimaryKeySql, pk).First();
    // }
 
    // public void SelectAlls()
    // {
    //     Context.Table<People>().ToArray();
    // }
 
    // public T[] SelectAll<T>() where T : new()
    // {
    //     return new TableQuery<T>(Context).ToArray();
    // }

}