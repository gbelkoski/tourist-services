using System.Text.Json.Serialization;
using Tourist.Domain;
using Tourist.Infrastructure;

namespace Tourist.Application.Commands;

public class CreateCustomerCommand : ICommand
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }
    [JsonPropertyName("Name")]
    public string Name { get; set; }
    [JsonPropertyName("Address")]
    public string Address { get; set; }
}

public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
{
    readonly IGenericRepository<Customer> _customerRepository;

    public CreateCustomerCommandHandler(IGenericRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task HandleAsync(CreateCustomerCommand command)
    {
        await _customerRepository.Insert(new Customer()
        {
            Id = command.Id,
            Name = command.Name, 
            Address = command.Address 
        });
    }
}