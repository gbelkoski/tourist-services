using Tourist.Domain;
using SQLite;

namespace Tourist.Infrastructure;
public class CustomerRepository : ICustomerRepository
{
    readonly DatabaseConfig _databaseConfig;

    public CustomerRepository(DatabaseConfig databaseConfig)
    {
        _databaseConfig = databaseConfig;
    }

    public async Task<Customer> Insert(Customer model)
    {
        var connection = new SQLiteAsyncConnection(_databaseConfig.ConnectionString);

        await connection.InsertAsync(model);
        
        return model;
    }
    public async Task<List<Customer>> SelectAll()
    {
        var connection = new SQLiteAsyncConnection(_databaseConfig.ConnectionString);

        return (await connection.QueryAsync<Customer>("SELECT Name, Address FROM Customer;")).ToList();
    }

    public async Task<Customer> SelectById(int id)
    {
        var connection = new SQLiteAsyncConnection(_databaseConfig.ConnectionString);
        return await connection.Table<Customer>().Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Customer> Update(Customer model)
    {
        var connection = new SQLiteAsyncConnection(_databaseConfig.ConnectionString);

        await connection.UpdateAsync(model);

        return model;
    }
}