using Tourist.Domain;
using Tourist.Infrastructure;
using Tourist.Domain;
using System.Threading.Tasks;

namespace Tourist.Application.Queries;
public class CustomersQueryHandler : IQueryHandler<CustomersQuery,IEnumerable<Customer>>
{
    //readonly IGenericRepository<Customer> _customerRepository;
    readonly ICustomerRepository _customerRepository;
    public CustomersQueryHandler(ICustomerRepository customerRepository)//IGenericRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> HandleAsync(CustomersQuery query)
    {
        return await _customerRepository.SelectAll();
    }
}