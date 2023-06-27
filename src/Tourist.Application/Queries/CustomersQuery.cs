using Tourist.Domain;
using Tourist.Infrastructure;

namespace Tourist.Application.Queries;
public class CustomersQuery : IQuery
{
}
public class CustomersQueryHandler : IQueryHandler<CustomersQuery, IEnumerable<Customer>>
{
    readonly IGenericRepository<Customer> _customersRepository;
    public CustomersQueryHandler(IGenericRepository<Customer> customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task<IEnumerable<Customer>> HandleAsync(CustomersQuery query)
    {
        return await _customersRepository.SelectAll();
    }
}