using Tourist.Domain;

namespace Tourist.Infrastructure;
public class GenericRepository<T> : IGenericRepository<T>
{
    public GenericRepository()
    {
        // var connectionString = "TouristServices.db";
        // _database = new SQLiteAsyncConnection(connectionString);
        // if(!_database.TableMappings.Any(c=>c.TableName=="Customer"))
        //     _database.CreateTableAsync<Customer>().Wait();
        // if(!_database.TableMappings.Any(c=>c.TableName=="ShipmentLineItem"))
        //     _database.CreateTableAsync<ShipmentLineItem>().Wait();
    }

    public T Insert(T model)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        
    }
 
    // public T Update<T>(T model)
    // {
    // }
 
    public async Task<bool> Delete<T>(object id)
    {
        throw new NotImplementedException();
    }
 
    // public T Select<T>(int pk) where T : new()
    // {
    // }
 
    // public void SelectAlls()
    // {
    // }
 
    public async Task<List<T>> SelectAll<T>() where T : new()
    {
        throw new NotImplementedException();
    }
}