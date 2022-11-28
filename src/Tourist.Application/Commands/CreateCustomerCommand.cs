using System.Text.Json.Serialization;
using Tourist.Domain;
using Tourist.Infrastructure;

namespace Tourist.Application.Commands;

public class CreateCustomerCommand : ICommand
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("Address")]
    public string Address { get; set; }
}

public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
{
    readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)//IGenericRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task HandleAsync(CreateCustomerCommand command)
    {
        _customerRepository.Insert(new Customer()
        {
            Id = Guid.NewGuid(),
            Name = command.Name, 
            Address = command.Address 
        });
    }
}