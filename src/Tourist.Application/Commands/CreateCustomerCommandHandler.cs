using Tourist.Infrastructure;
using Tourist.Domain;

namespace Tourist.Application.Commands;

public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
{
    //readonly IGenericRepository<Customer> _customerRepository;
    readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository)//IGenericRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }
    // private class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    // {
    //     public CreateCustomerCommandValidator()
    //     {
    //         RuleFor(x => x).CommandNotNullValidation();
    //         When(x => x != null, () =>
    //         {
    //             RuleFor(x => x.Id).IdNotEmptyValidation();
    //             RuleFor(x => x.Name).UsernameNotEmptyValidation();
    //         });
    //     }
    // }
        
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