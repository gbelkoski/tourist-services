using Tourist.Domain;

namespace Tourist.Infrastructure;
public interface ICustomerRepository
{
    Task<Customer> Insert(Customer model);        
    Task<Customer> Update(Customer model);
    // Task<bool> Delete(Guid id);
    // T Select<T>(int pk) where T : new();
    Task<Customer> SelectById(int id);
    Task<List<Customer>> SelectAll();
}