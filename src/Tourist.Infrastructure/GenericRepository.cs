using SQLite;

namespace Tourist.Infrastructure;
public class GenericRepository
{
    readonly SQLiteAsyncConnection database;

    public GenericRepository()
    {
        var connectionString = "Data Source=AppData/TouristServices.db;Version=3;";
        database = new SQLiteAsyncConnection(connectionString);
        database.CreateTableAsync<Customer>().Wait();
        database.CreateTableAsync<ItemLedger>().Wait();
        database.CreateTableAsync<Package>().Wait();
        database.CreateTableAsync<ShipmentItem>().Wait();
    }
 
    // public T Insert<T>(T model)
    // {
    //     int iRes = Context.Insert(model);
    //     return model;
    // }
 
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