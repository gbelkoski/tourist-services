using SQLite;

namespace Tourist.Infrastructure;
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    readonly SQLiteAsyncConnection _dbConnection;

    public GenericRepository(DatabaseConfig config)
    {
        _dbConnection = new SQLiteAsyncConnection(config.Database);
    }

    public async Task<T> Insert(T model)
    {
        await _dbConnection.InsertAsync(model);

        return model;
    }

    public async Task<List<T>> SelectAll()
    {
        throw new NotImplementedException();
        //return await _dbConnection.QueryAsync<T>();
    }

    public async Task<T> SelectById(int id)
    {
        throw new NotImplementedException();
        //return await _dbConnection.GetAsync(id);
    }

    public async Task<T> Update(T model)
    {
        await _dbConnection.UpdateAsync(model);

        return model;
    }
}
