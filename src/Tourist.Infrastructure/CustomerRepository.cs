using Tourist.Domain;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

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
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);

        await connection.ExecuteAsync("INSERT INTO Customer (Id, Name, Address)" +
            "VALUES (@Id, @Name, @Address);", model);
        
        return model;
    }
    public async Task<List<Customer>> SelectAll()
    {
        using var connection = new SqliteConnection(_databaseConfig.ConnectionString);
 
        return (await connection.QueryAsync<Customer>("SELECT Name, Address FROM Customer;")).ToList();
    }
}