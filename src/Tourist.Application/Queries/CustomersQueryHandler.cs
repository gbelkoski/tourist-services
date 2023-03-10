using Tourist.Domain;
using Tourist.Infrastructure;

namespace Tourist.Application.Queries;
public class CustomersQueryHandler : IQueryHandler<CustomersQuery,IEnumerable<Customer>>
{
    readonly IGenericRepository<Customer> _customerRepository;
    public CustomersQueryHandler(IGenericRepository<Customer> customerRepository)//IGenericRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> HandleAsync(CustomersQuery query)
    {
        return await _customerRepository.SelectAll();
    }
}