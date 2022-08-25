namespace Tourist.Infrastructure;
public interface IGenericRepository : IDisposable
{
    T Insert<T>(T model);        
    T Update<T>(T model);
    bool Delete<T>(T model);
    T Select<T>(int pk) where T : new();
    T[] SelectAll<T>() where T : new();
}