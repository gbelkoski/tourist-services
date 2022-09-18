namespace Tourist.Infrastructure;
public interface IGenericRepository<T> : IDisposable
{
    T Insert(T model);        
    // T Update<T>(T model);
    Task<bool> Delete<T>(object id);
    // T Select<T>(int pk) where T : new();
    Task<List<T>> SelectAll<T>() where T : new();
}