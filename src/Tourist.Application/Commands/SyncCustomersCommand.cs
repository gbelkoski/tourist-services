using System.Text.Json.Serialization;
using Tourist.Domain;
using Tourist.Infrastructure;

namespace Tourist.Application.Commands;

public class SyncCustomersCommand : ICommand
{
    public List<Customer> Customers { get; set; }
}

public class SyncCustomersCommandHandler : ICommandHandler<SyncCustomersCommand>
{
    readonly ICustomerRepository _customerRepository;

    public SyncCustomersCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task HandleAsync(SyncCustomersCommand command)
    {
        foreach(var customer in command.Customers)
        {
            var dbCustomer = await _customerRepository.SelectById(customer.Id);
            if(dbCustomer == null)
            {
                await _customerRepository.Insert(new Customer()
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Address = customer.Address,
                    IsDeleted = customer.IsDeleted
                });
            }
            else
            {
                dbCustomer.Name = customer.Name;
                dbCustomer.Address = customer.Address;
                dbCustomer.IsDeleted = customer.IsDeleted;
                await _customerRepository.Update(dbCustomer);
            }
        }
    }
}