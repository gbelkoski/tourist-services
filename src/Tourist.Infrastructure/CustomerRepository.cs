using Tourist.Domain;
using SQLite;

namespace Tourist.Infrastructure;
public class CustomerRepository : IGenericRepository<Customer>
{
    readonly SQLiteAsyncConnection _dbConnection;

    public CustomerRepository(DatabaseConfig databaseConfig)
    {
        _dbConnection = new SQLiteAsyncConnection(databaseConfig.Database);
    }

    public async Task<Customer> Insert(Customer model)
    {
        await _dbConnection.InsertAsync(model);
        return model;
    }

    public async Task<List<Customer>> SelectAll()
    {
        return (await _dbConnection.QueryAsync<Customer>("SELECT * FROM Customer;")).ToList();
    }

    public async Task<Customer> SelectById(int id)
    {
        return await _dbConnection.Table<Customer>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Customer> Update(Customer model)
    {
        await _dbConnection.UpdateAsync(model);
        return model;
    }
}