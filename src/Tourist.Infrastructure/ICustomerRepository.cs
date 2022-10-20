using Tourist.Domain;

namespace Tourist.Infrastructure;
public interface ICustomerRepository
{
    Task<Customer> Insert(Customer model);        
    // T Update<T>(T model);
    // Task<bool> Delete(Guid id);
    // T Select<T>(int pk) where T : new();
    Task<List<Customer>> SelectAll();
}