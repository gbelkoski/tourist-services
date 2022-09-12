//using FluentValidation;
using Tourist.Infrastructure;
using Tourist.Domain;

namespace Tourist.Application.Commands;

public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand>
{
    readonly IGenericRepository<Customer> _customerRepository;

    public CreateCustomerCommandHandler(IGenericRepository<Customer> customerRepository)
    {
        _customerRepository = customerRepository;
    }
    // private class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    // {
    //     public CreateCustomerCommandValidator()
    //     {
    //         // RuleFor(x => x).CommandNotNullValidation();
    //         // When(x => x != null, () =>
    //         // {
    //         //     RuleFor(x => x.NotificationId).IdNotEmptyValidation();
    //         //     RuleFor(x => x.Username).UsernameNotEmptyValidation();
    //         // });
    //     }
    // }
        
    public async Task HandleAsync(CreateCustomerCommand command)
    {
        _customerRepository.Insert(new Customer(){ Name = command.Name });
    }
}